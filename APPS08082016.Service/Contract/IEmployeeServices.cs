using APPS08082016.Core.DTO;
using APPS08082016.Core.Response;

namespace APPS08082016.Service.Contract
{
    public interface IEmployeeServices
    {
        OperationListResponse<EmployeeInfo> GetEmployeeInfo();
    }
}

/* # sample data remove please

import json
from fuzzywuzzy import fuzz

# Load data from JSON file
with open('extracted_data.json', 'r') as file:
    data = json.load(file)

# Define groups and parent key fields
client_fields = ["Client_Name", "Client_Billing_Street", "Client_Billing_City", "Client_Billing_Zip", "Client_Billing_State", "Client_Billing_Country", "Client_Email_Address"]
broker_fields = ["Broker_Agency_Name", "Broker_Producer_Contact_Name", "Broker_Producer_Street", "Broker_Producer_City", "Broker_Producer_Zip", "Broker_Producer_State", "Broker_Producer_Country", "Broker_Phone_Number", "Broker_Producer_Email_Address"]
other_fields = ["Client_SIC", "Client_NAICS", "Client_FEIN", "Policy_Line_of_Business", "Policy_Effective_Date", "Policy_Expiration_Date"]

client_key = "Client_Name"
broker_key = "Broker_Agency_Name"

# Filter data
filtered_data = [entry for entry in data if entry['type'].startswith('Acord') or entry['type'].startswith('Applied')]

# Function to perform fuzzy matching and fill missing data
def fuzzy_fill(data_list, key):
    for entry in data_list:
        if not entry['extracted_data'].get(key):
            best_match = None
            highest_score = 0
            for other_entry in data_list:
                if other_entry != entry and other_entry['extracted_data'].get(key):
                    score = fuzz.ratio(entry['extracted_data'].get(key, ''), other_entry['extracted_data'].get(key, ''))
                    if score > highest_score:
                        highest_score = score
                        best_match = other_entry
            if best_match:
                entry['extracted_data'][key] = best_match['extracted_data'][key]

# Function to get best match for a key field
def get_best_match(data_list, key):
    best_match = None
    highest_score = 0
    for entry in data_list:
        score = fuzz.ratio(entry['extracted_data'].get(key, ''), entry['extracted_data'].get(key, ''))
        if score > highest_score:
            highest_score = score
            best_match = entry
    return best_match

# Function to merge data from different sources
def merge_data(data_list, fields):
    merged_data = {}
    for field in fields:
        for entry in data_list:
            if entry['extracted_data'].get(field) and entry['extracted_data'][field].strip():
                merged_data[field] = entry['extracted_data'][field]
                break
    return merged_data

# Get best matches for client and broker groups
client_best_match = get_best_match(filtered_data, client_key)
broker_best_match = get_best_match(filtered_data, broker_key)

# Fill missing data for client and broker groups
fuzzy_fill(filtered_data, client_key)
fuzzy_fill(filtered_data, broker_key)

# Merge data for client and broker groups
client_data = merge_data(filtered_data, client_fields)
broker_data = merge_data(filtered_data, broker_fields)

# Merge data for other group
other_data = merge_data(filtered_data, other_fields)

# Combine all groups into a single object
result = {**client_data, **broker_data, **other_data}

print(json.dumps(result, indent=4))

*/
