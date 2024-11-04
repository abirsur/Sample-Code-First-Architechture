using APPS08082016.Core.DTO;
using APPS08082016.Core.Response;

namespace APPS08082016.Service.Contract
{
    public interface IEmployeeServices
    {
        OperationListResponse<EmployeeInfo> GetEmployeeInfo();
    }
}

#------------------------------------------
    import os
import sys
from fastapi import FastAPI
from contextlib import asynccontextmanager
from router import router, receive_messages

@asynccontextmanager
async def lifespan(app: FastAPI):
    # Startup event
    receive_messages()
    yield
    # Shutdown event
    print("Shutting down the application.")

app = FastAPI(lifespan=lifespan)
app.include_router(router)

@app.get("/health")
async def health_check():
    return {"status": "ok"}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)


        #===============================================
# deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: your-app-deployment
spec:
  replicas: 0  # Initial number of replicas
  selector:
    matchLabels:
      app: your-app
  template:
    metadata:
      labels:
        app: your-app
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
        livenessProbe:
          httpGet:
            path: /health
            port: 8000
          initialDelaySeconds: 60
          periodSeconds: 30
          timeoutSeconds: 10
        readinessProbe:
          httpGet:
            path: /health
            port: 8000
          initialDelaySeconds: 60
          periodSeconds: 30
          timeoutSeconds: 10
        resources:
          requests:
            memory: "256Mi"
            cpu: "500m"
          limits:
            memory: "512Mi"
            cpu: "1"

        
