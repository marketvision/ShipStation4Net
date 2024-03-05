#### 1.0.8 - 04.03.2024
* Parse apiLimitRemaining &amp; limitResetSeconds headers only on 429 response + a bit more refactoring

#### 1.0.7 - 13.12.2023
* Made interfaces public to simplify unit testing

#### 1.0.6 - 12.12.2023
* Added IGetsResourceUrlResponses to Fulfillments

#### 1.0.5 - 14.07.2023
* xcover &amp; parcelguard insurance options added to enum

#### 1.0.4 - 14.12.2022
* Added ProductType entity and changed property on Product entity

#### 1.0.3 - 05.10.2022
* Fixed issue with Void Shipment Label api endpoint

#### 1.0.2 - 22.06.2022
* Updated Newtonsoft.Json to 13.0.1

#### 1.0.1 - 29.12.2021
* Updated product length, width, height, weigthOz from int to double

#### 1.0.0 - 28.12.2021
* Updated to match latest api documentation
* Added support for Webhook api endpoints
* Added support for multiple orders create/update

#### 0.10.76 - 07.07.2021
* Fixed issue with deserialization when ShipStation returns weight in inches - ignore weight units that are not expected

#### 0.10.75 - 21.04.2021
* Updated System.Net.Http to 4.3.4

#### 0.10.74 - 30.04.2020
* Fix time zone string for OSX

#### 0.10.73 - 05.04.2020
* Added support for retreving webhook payloads

#### 0.10.72 - 07.11.2019
* Added "provider" to InsuranceOptionProviders enum

#### 0.10.71 - 17.07.2019
* Fixes model issue with shipmentItems options

#### 0.10.70 - 13.07.2018
* Added Stores.RefreshAllStoresAsync method on client
