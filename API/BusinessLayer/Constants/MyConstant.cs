namespace CREL_BE.Helpers
{
    public static class MyConstant
    {
        public const string apiVersion = "v1.0";

        public static class Notification
        {
            public static class Status { 
                public const int Deleted = 0;
            }

            public static class Type
            {
                public const int AppointmentReminder = 1;
                public const int AppointmentCreation = 2;
                public const int PropertyForRentApprove = 3;
                public const int PropertyForRentUnapprove = 4;
                public const int PropertyForRentAssign = 5;
                public const int PropertyForRentUnassign = 6;
                public const int ContractReminder = 7;
                public const int TeamAdd = 8;
                public const int TeamRemove = 9;
                public const int BrandAssign = 10;
                public const int BrandUnassign = 11;
                public const int AppointmentWaiting = 12;
            }
            public static class Filter
            {
                public const int SendDateAscending = 1;
                public const int SendDateDescending = 2;
            }
        }
        public static class Appointment
        {
            public static class Status
            {
                public const int Waiting = 1;
                public const int Approved = 2;
                public const int Rejected = 3;

                public const int Deleted = 0; 
            }
        }
        public static class PropertyForRent
        {
            public static class Status
            {
                public const int Created = 1;
                public const int Approved = 2;
                public const int Rented = 3;
                public const int Processing = 4;
                public const int Rejected = 5;
                public const int Deleted = 0;
            }
        }
        public static class Admin
        {
            public static class Gender
            {
                public static bool Male = true;
                public static bool Female = false;
            }
            public static class Status{
                public const int Created = 1;
                public const int Approved = 2;
                public const int Deleted = 0; 
            }
        }
        public static class Media{
            public static class Type{
                public const int InImakit = 1;
                public const int Orther = 2;
            }
        }
        public static class Staff{
            public static class Gender
            {
                public static bool Male = true;
                public static bool Female = false;
            }
            public static class Status{
                public const int Created = 1;
                public const int Approved = 2;
                public const int Deleted = 0; 
            }
        }

        public static class Brand{
            public static class Status{
                public const int Created = 1;
                public const int Approved = 2;
                public const int Deleted = 0; 
            }
        }

        public static class Building{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class BuildingType{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class City{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Owner{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class District{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Industry{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Poi{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Property{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Project{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Space{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Store{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Street{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class StreetSegment{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Team{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Ward{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class AreaManager{
            public static class Status{
                public const int Deleted = 0; 
            }
        }

        public static class Broker{
            public static class Status{
                public const int Deleted = 0; 
                public const int Created = 1; 
                public const int HaveTeam = 2; 
            }
        }

    }
}