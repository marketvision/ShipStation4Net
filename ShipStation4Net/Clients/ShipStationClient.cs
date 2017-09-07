#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

namespace ShipStation4Net.Clients
{
    public class ShipStationClient
    {
        protected readonly Configuration Configuration;

        public ShipStationClient(Configuration configuration) => Configuration = configuration;

        private Accounts _Accounts;

        public Accounts Accounts
        {
            get
            {
                if (_Accounts == null)
                {
                    _Accounts = new Accounts(Configuration);
                }

                return _Accounts;
            }
        }

        private Carriers _Carriers;

        public Carriers Carriers
        {
            get
            {
                if (_Carriers == null)
                {
                    _Carriers = new Carriers(Configuration);
                }

                return _Carriers;
            }
        }

        private Customers _Customers;

        public Customers Customers
        {
            get
            {
                if (_Customers == null)
                {
                    _Customers = new Customers(Configuration);
                }

                return _Customers;
            }
        }
        
        private Fulfillments _Fulfillments;

        public Fulfillments Fulfillments
        {
            get
            {
                if (_Fulfillments == null)
                {
                    _Fulfillments = new Fulfillments(Configuration);
                }

                return _Fulfillments;
            }
        }

        private Orders _Orders;

        public Orders Orders
        {
            get
            {
                if (_Orders == null)
                {
                    _Orders = new Orders(Configuration);
                }

                return _Orders;
            }
        }

        private Products _Products;

        public Products Products
        {
            get
            {
                if (_Products == null)
                {
                    _Products = new Products(Configuration);
                }

                return _Products;
            }
        }

        private Shipments _Shipments;

        public Shipments Shipments
        {
            get
            {
                if (_Shipments == null)
                {
                    _Shipments = new Shipments(Configuration);
                }

                return _Shipments;
            }
        }

        private Stores _Stores;

        public Stores Stores
        {
            get
            {
                if (_Stores == null)
                {
                    _Stores = new Stores(Configuration);
                }

                return _Stores;
            }
        }

        private Users _Users;

        public Users Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = new Users(Configuration);
                }

                return _Users;
            }
        }

        private Warehouses _Warehouses;

        public Warehouses Warehouses
        {
            get
            {
                if (_Warehouses == null)
                {
                    _Warehouses = new Warehouses(Configuration);
                }

                return _Warehouses;
            }
        }

    }
}
