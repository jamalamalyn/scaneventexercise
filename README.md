# Scan Event Exercise
### Scan Event Exercise - test for Freightways Information Services

----

## Objective
- to create a worker application that consumes a scan event API (see model above) and records the last event made against a parcel and any pickup or delivery times.

----

## Requirements
1. The application must consume from a scan event API and keep a track of the last event that was
fetched so that it can continue where it left off if the service is stopped and started.
2. The application should persist the scan event data in such a way that the following information could
later be fetched easily;
  a. The most recent scan event against a parcel, specifically only values from fields; EventId,
ParcelId, Type, CreatedDateTimeUtc, StatusCode, RunId are required.
  b. DateTimes indicating when a parcel has been;
    i. Picked up (a scan event of Type ‘PICKUP’ has occurred).
    ii. Delivered (a scan event of Type ‘DELIVERY’ has occurred).
3. The application should be fault tolerant and resilient (e.g. handle new event types, malformed data).
4. The application should contain appropriate logging.

----

## Data model
```json
{
    "ScanEvents": [
        {
            "EventId": 83269,
            "ParcelId": 5002,
            "Type": "PICKUP",
            "CreatedDateTimeUtc": "2021-05-11T21:11:34.1506147Z",
            "StatusCode": "",
            "Device": {
                "DeviceTransactionId": 83269,
                "DeviceId": 103
            },
            "User": {
                "UserId": "NC1001",
                "CarrierId": "NC",
                "RunId": "100"
            }
        }
    ]
}
```
- EventId : Unique Event ID
- ParcelId : The Parcel The Scan Event Applies To
- Type : PICKUP, STATUS, DELIVERY
- CarrierId : NC, PH, CP, NW

---

## Steps to run

## Scan Event API detail
1. For the purposes of development, you can assume a Scan Event API exists that has a single endpoint
available e.g. GET http://localhost/v1/scans/scanevents
2. The scan event API endpoint above supports the following URL parameters;
  a. FromEventId – return scan events with an EventId greater than or equal to this (defaults to 1).
  b. Limit – the total number of scan events to return (defaults to 100). e.g. http://localhost/v1/scans/scanevents?FromEventId=83269&Limit=100
3. Sample Scan Event JSON;

----

## Assumptions & Exclusions
- I assume that the API endpoint the Worker Service is consuming from is stored in a raw state perhaps from a message queue
- I assume that the frequency that the Worker Service consumes the API should be as often as possible. Package delivery data is mission critical, and its important information to be consumed, prepared in a persistent state relationally - ready for specific details to be retrieved for normal business operations. (e.g. customer alerts for package delivery for example)
- The worker service is a background service, and as such there are no end-users apart from Developers who need to do diagnostics/debugging - hence the need for robust logging.
- I assume that there is no need for delete and update functionality
- I assume that new Event types can be 

---

## Exclusions
- The purpose of this application is for data storage in a way that is easy to be fetched later. Presenting the data is out of scope.
---

## Future improvements & extensions
- As it uses a local SqLite database implementation for data storage, moving the data off into a cloud based data storage would be far better - considering the possible large volumes of data that can be stored.
