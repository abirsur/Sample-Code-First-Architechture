using APPS08082016.Core.DTO;
using APPS08082016.Core.Response;

namespace APPS08082016.Service.Contract
{
    public interface IEmployeeServices
    {
        OperationListResponse<EmployeeInfo> GetEmployeeInfo();
    }
}
#+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    import os
import sys
from fastapi import FastAPI
from fastapi.middleware.lifespan import LifespanMiddleware
from contextlib import asynccontextmanager
from router import router, receive_messages

app = FastAPI()

@asynccontextmanager
async def lifespan(app: FastAPI):
    # Startup event
    receive_messages()
    yield
    # Shutdown event
    print("Shutting down the application.")

app.add_middleware(LifespanMiddleware, lifespan=lifespan)
app.include_router(router)

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)

#-----------------------------------------------------
        import os
import sys
from fastapi import APIRouter
from azure.servicebus import ServiceBusClient, ServiceBusMessage
from azure.storage.blob import BlobServiceClient

router = APIRouter()

CONNECTION_STR = os.getenv("SERVICE_BUS_CONNECTION_STRING")
QUEUE_NAME = os.getenv("SERVICE_BUS_QUEUE_NAME")
RESPONSE_QUEUE_NAME = os.getenv("SERVICE_BUS_RESPONSE_QUEUE_NAME")
BLOB_CONNECTION_STR = os.getenv("BLOB_CONNECTION_STRING")

servicebus_client = ServiceBusClient.from_connection_string(conn_str=CONNECTION_STR)
blob_service_client = BlobServiceClient.from_connection_string(BLOB_CONNECTION_STR)

def process_message(message):
    try:
        # Read data from the message
        data = message.body.decode('utf-8')
        # Process the data
        result = process_data(data)
        # Log the response
        log_response(result)
    except Exception as e:
        # Log the exception
        log_response(str(e))
    finally:
        try:
            message.complete()
        except Exception as e:
            log_response(f"Failed to complete message: {str(e)}")

def process_data(data):
    # Implement your data processing logic here
    return f"Processed data: {data}"

def log_response(response):
    try:
        with servicebus_client.get_queue_sender(queue_name=RESPONSE_QUEUE_NAME) as sender:
            response_message = ServiceBusMessage(response)
            sender.send_messages(response_message)
    except Exception as e:
        print(f"Failed to log response: {str(e)}")

def receive_messages():
    try:
        with servicebus_client.get_queue_receiver(queue_name=QUEUE_NAME) as receiver:
            messages = receiver.receive_messages(max_message_count=10, max_wait_time=5)
            if not messages:
                print("No more messages to process. Exiting.")
                sys.exit(0)
            for message in messages:
                process_message(message)
    except Exception as e:
        print(f"Failed to receive messages: {str(e)}")

def download_blob(container_name, blob_name, file_name):
    try:
        container_client = blob_service_client.get_container_client(container_name)
        blob_client = container_client.get_blob_client(blob_name)
        with open(file_name, "wb") as download_file:
            download_file.write(blob_client.download_blob().readall())
        print(f"Downloaded {blob_name} to {file_name}")
    except Exception as e:
        print(f"Failed to download blob: {str(e)}")

  #--------------------------------------------
            # job.yaml
apiVersion: batch/v1
kind: Job
metadata:
  name: your-app-job
spec:
  ttlSecondsAfterFinished: 100  # Time to live after job completion
  template:
    spec:
      containers:
      - name: your-app
        image: <your-registry>/your-app:latest
        env:
        - name: SERVICE_BUS_CONNECTION_STRING
          valueFrom:
            secretKeyRef:
              name: service-bus-secret
              key: connection-string
        - name: SERVICE_BUS_QUEUE_NAME
          value: "<your-service-bus-queue-name>"
        - name: SERVICE_BUS_RESPONSE_QUEUE_NAME
          value: "<your-service-bus-response-queue-name>"
        - name: BLOB_CONNECTION_STRING
          valueFrom:
            secretKeyRef:
              name: blob-service-secret
              key: connection-string
      restartPolicy: Never
  backoffLimit: 0
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
#-----------------------------------------------------------------------------

import os
import sys
from azure.servicebus import ServiceBusClient, ServiceBusMessage
from azure.storage.blob import BlobServiceClient

CONNECTION_STR = os.getenv("SERVICE_BUS_CONNECTION_STRING")
QUEUE_NAME = os.getenv("SERVICE_BUS_QUEUE_NAME")
RESPONSE_QUEUE_NAME = os.getenv("SERVICE_BUS_RESPONSE_QUEUE_NAME")
BLOB_CONNECTION_STR = os.getenv("BLOB_CONNECTION_STRING")

servicebus_client = ServiceBusClient.from_connection_string(conn_str=CONNECTION_STR)
blob_service_client = BlobServiceClient.from_connection_string(BLOB_CONNECTION_STR)

def process_message(message):
    try:
        # Read data from the message
        data = message.body.decode('utf-8')
        # Process the data
        result = process_data(data)
        # Log the response
        log_response(result)
    except Exception as e:
        # Log the exception
        log_response(str(e))
    finally:
        try:
            message.complete()
        except Exception as e:
            log_response(f"Failed to complete message: {str(e)}")

def process_data(data):
    # Implement your data processing logic here
    return f"Processed data: {data}"

def log_response(response):
    try:
        with servicebus_client.get_queue_sender(queue_name=RESPONSE_QUEUE_NAME) as sender:
            response_message = ServiceBusMessage(response)
            sender.send_messages(response_message)
    except Exception as e:
        print(f"Failed to log response: {str(e)}")

def receive_messages():
    try:
        with servicebus_client.get_queue_receiver(queue_name=QUEUE_NAME) as receiver:
            messages = receiver.receive_messages(max_message_count=10, max_wait_time=5)
            if not messages:
                print("No more messages to process. Exiting.")
                sys.exit(0)
            for message in messages:
                process_message(message)
    except Exception as e:
        print(f"Failed to receive messages: {str(e)}")

def download_blob(container_name, blob_name, file_name):
    try:
        container_client = blob_service_client.get_container_client(container_name)
        blob_client = container_client.get_blob_client(blob_name)
        with open(file_name, "wb") as download_file:
            download_file.write(blob_client.download_blob().readall())
        print(f"Downloaded {blob_name} to {file_name}")
    except Exception as e:
        print(f"Failed to download blob: {str(e)}")

if __name__ == "__main__":
    receive_messages()
-----------------------------------------------------------------------------------------------------

# Dockerfile
FROM python:3.9-slim

WORKDIR /app

COPY pyproject.toml poetry.lock /app/
RUN pip install poetry && poetry install --no-dev

COPY . /app

CMD ["poetry", "run", "python", "app.py"]
------------------------------------------------------------------------------------------------------
# job.yaml
apiVersion: batch/v1
kind: Job
metadata:
  name: your-app-job
spec:
  ttlSecondsAfterFinished: 100  # Time to live after job completion
  template:
    spec:
      containers:
      - name: your-app
        image: <your-registry>/your-app:latest
        env:
        - name: SERVICE_BUS_CONNECTION_STRING
          valueFrom:
            secretKeyRef:
              name: service-bus-secret
              key: connection-string
        - name: SERVICE_BUS_QUEUE_NAME
          value: "<your-service-bus-queue-name>"
        - name: SERVICE_BUS_RESPONSE_QUEUE_NAME
          value: "<your-service-bus-response-queue-name>"
      restartPolicy: Never
  backoffLimit: 0
  
----------------------------------------------------------------------------------------
# scaledobject.yaml
apiVersion: keda.sh/v1alpha1
kind: ScaledObject
metadata:
  name: your-app-scaledobject
spec:
  scaleTargetRef:
    kind: Job
    name: your-app-job
  triggers:
  - type: azure-servicebus
    metadata:
      namespace: <your-namespace>
      queueName: <your-service-bus-queue-name>
      connection: SERVICE_BUS_CONNECTION_STRING
      messageCount: "1"  # Scale when there is at least 1 message in the queue
