using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using transitProject.Models;

namespace transitProject.DAL
{
    public class TransitIntitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TransitContext>
    {
        protected override void Seed(TransitContext context)
        {
            //---------------------------------------------------------------------------------
            //Initialize Stops
            var stopList = new List<Stop>

            {
                new Stop() {stopId=11,stopName="First Gulf",source="Brampton Terminal",destination="Humber College",eta=TimeSpan.Parse("11:00")},
                new Stop() {stopId=12,stopName="Rutherford",source="Brampton Terminal",destination="Humber College",eta=TimeSpan.Parse("11:05")},
                new Stop() {stopId=13,stopName="Kennedy",source="Brampton Terminal",destination="Humber College",eta=TimeSpan.Parse("11:10")},

                new Stop() {stopId=21,stopName="Hurontario",source="Brampton Terminal",destination="Square One",eta=TimeSpan.Parse("11:30")},
                new Stop() {stopId=22,stopName="Mclaughlin Rd",source="Brampton Terminal",destination="Square One",eta=TimeSpan.Parse("11:35")},
                new Stop() {stopId=23,stopName="Ray Lawson",source="Brampton Terminal",destination="Square One",eta=TimeSpan.Parse("11:40")},

                new Stop() {stopId=31,stopName="Creditview",source="Mount Pleasant Go",destination="Humber College",eta=TimeSpan.Parse("10:00")},
                new Stop() {stopId=32,stopName="Williams Parkway",source="Mount Pleasant Go",destination="Humber College",eta=TimeSpan.Parse("10:05")},
                new Stop() {stopId=33,stopName="Chinguacousy",source="Mount Pleasant Go",destination="Humber College",eta=TimeSpan.Parse("10:10")},

                new Stop() {stopId=41,stopName="Bovaird",source="Mount Pleasant Go",destination="Square One",eta=TimeSpan.Parse("12:20")},
                new Stop() {stopId=42,stopName="Queen St",source="Mount Pleasant Go",destination="Square One",eta=TimeSpan.Parse("12:30")},
                new Stop() {stopId=43,stopName="Charolais",source="Mount Pleasant Go",destination="Square One",eta=TimeSpan.Parse("12:35")}

            };
            stopList.ForEach(s => context.Stops.Add(s));
            context.SaveChanges();
            //---------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------
            //Initialize Admins

            var adminList = new List<Admin>
            {
                new Admin() {adminId=1,userName="admin1",password="11111"},
                new Admin() {adminId=2,userName="admin2",password="22222"},
                new Admin() {adminId=3,userName="admin3",password="33333"}
            };
            adminList.ForEach(s => context.Admins.Add(s));
            context.SaveChanges();
            //---------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------
            //Initialize Owner

            var ownerList = new List<Owner>
                {
                new Owner() {userName="root",password="root"}
               
            };
            ownerList.ForEach(s => context.Owners.Add(s));
            context.SaveChanges();
            //---------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------
            //Initilize Map Locations

            var cityList = new List<City>
                {
                new City() {Title = "Brampton", Lat = 43.731500, Lng = -79.7624},
                new City() {Title = "Mississauga", Lat = 43.5890, Lng = -79.6441 },
                new City() {Title = "Etobicoke", Lat = 43.6205, Lng = -79.5132 },
                new City() {Title = "Brampton Terminal", Lat = 43.6655256,  Lng = -79.7322502},
                new City() { Title = "Toronto", Lat = 43.6532, Lng = -79.3832},
                new City() {Title = "Square One", Lat = 43.6101784, Lng = -79.6764421 },
                new City() {Title = "Humber College North Campus", Lat = 43.7285848, Lng = -79.6107236 },
                new City() {Title = "Mount Pleasant Go", Lat = 43.6756534, Lng = -79.8218008},
                new City() {Title = "First Gulf - Zum Steeles Station Stop EB", Lat = 43.6809689, Lng = -79.7186347},
                new City() {Title = "Rutherford - Zum Steeles Station Stop WB", Lat = 43.6803169, Lng = -79.7200387},
                new City() {Title = "Kennedy - Zum Steeles Station Stop WB", Lat = 43.6749229, Lng = -79.7241047 },
                new City() {Title = "Steeles Ave E e/of Hurontario St", Lat = 43.6652409, Lng = -79.7347407 },
                new City() {Title = "Steeles Ave W at McLaughlin Rd S", Lat = 43.6560737, Lng = -79.743651 },
                new City() {Title = "Hurontario St At Ray Lawson Blvd", Lat = 43.6584281, Lng = -79.725037 },
                new City() {Title = "Main St. N. @ Bovaird Dr. W.", Lat = 43.7043407, Lng = -79.7905787 }


            };
            cityList.ForEach(s => context.Cities.Add(s));
            context.SaveChanges();

            //---------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------
            //Initialize Events

            var eventList = new List<Event>
                {
                new Event() { EventId =1,Subject="Free Rides! It's Christmas",Description="Get Free Rides from 12pm to 5pm",Start=DateTime.Parse("2020-12-25 12:00:00"),End=DateTime.Parse("2020-12-25 17:00:00"),ThemeColor="Green",IsFullDay=false},
                new Event() { EventId =2,Subject="Half Rates! New Year's Eve",Description="Ride at half fare on New Year's eve from 10am to 8pm",Start=DateTime.Parse("2020-12-31 10:00:00"),End=DateTime.Parse("2020-12-31 20:00:00"),ThemeColor="Blue",IsFullDay=true }

            };
            eventList.ForEach(s => context.Events.Add(s));
            context.SaveChanges();
            //---------------------------------------------------------------------------------

         


        }
    }
}