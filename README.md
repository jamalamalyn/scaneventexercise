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

----
## Assumptions, Exclusions & Dependencies

---
## Future improvements & extensions
