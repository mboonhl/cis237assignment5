//Author: Morgan Boon
//CIS 237
//Assignment 5
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class RepositoryAPI : IWineCollection
    {
        
        //Variables to hold wine items
        BeverageMBoonEntities beverageEntities;

        //Varibale to hold length
        private int wineItemsLength;

        //Variable for header/footer
        private string header = new string('*', 125);

        private string headed2 = "ID".PadRight(10) + " " + "Beverage Discription".PadRight(75) + " " +
                                 "Pack".PadRight(20) +
                                 " " + "Price".PadRight(10) + " " + "Active";

        //Constuctor. Must pass the size of the collection.
        public RepositoryAPI()
        {
            //Create new instance of the BeverageEnteries Class
            beverageEntities = new BeverageMBoonEntities();

            wineItemsLength = beverageEntities.Beverages.Count();
        }

        //Add a new item to the collection
        public bool AddNewItem(string _id, string _name, string _pack, decimal _price, bool _active)
        {
            bool itemAdded = false;
            bool alreadyExists = false;

            Beverage wineToAdd = new Beverage();
            wineToAdd.id = _id;
            wineToAdd.name = _name;
            wineToAdd.pack = _pack;
            wineToAdd.price = _price;
            wineToAdd.active = _active;

            //Checks Each Item In The Wine List to see if the id already exists
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                //If the id already exists sets alreadyExists bool to true
                if (beverage.id == _id)
                {
                    alreadyExists = true;
                }
            }

            //If the id is not in the database it continues to add it to the database
            if (alreadyExists == false)
            {
                try
                {
                    //Adds beverage to the database
                    beverageEntities.Beverages.Add(wineToAdd);

                    //Saves changes to the database
                    beverageEntities.SaveChanges();

                    //Increases the value of the wineItemsLength
                    wineItemsLength++;

                    //If everything worked changes the itemAdded bool to true
                    itemAdded = true;

                }
                catch (Exception exception)
                {
                    //Removes Item from database if not added successfully
                    beverageEntities.Beverages.Remove(wineToAdd);

                    Console.WriteLine("Wine Cannot be added...");
                    Console.WriteLine(exception.Message);

                    //Since Item failed set item add failed itemAdded is set to false
                    itemAdded = false;

                }
            }

            return itemAdded;
        }

        //Get The Print String Array For All Items
        public string[] GetPrintStringsForAllItems()
        {
            //Create and array to hold all of the printed strings
            string[] allItemStrings = new string[wineItemsLength];
            //set a counter to be used
            int counter = 0;

            //If the wineItemsLength is greater than 0, create the array of strings
            if (wineItemsLength > 0)
            {
                Console.WriteLine(header);
                Console.WriteLine(headed2);

                //For each item in the collection
                foreach (Beverage beverage in beverageEntities.Beverages)
                {
                        //Add the results of calling ToString on the item to the string array.
                    allItemStrings[counter] = beverage.id.Trim().PadRight(10) + " " + beverage.name.Trim().PadRight(75) + " " +
                                              beverage.pack.Trim().PadRight(20) +
                                              " " + beverage.price.ToString("C").PadRight(10) + " " + beverage.active;
                        counter++;
                    
                }
            }
            //Return the array of item strings
            return allItemStrings;
        }

        //Find an item by it's Id
        public string FindById(string _id)
        {
            //Declare return string for the possible found item
            string returnString = null;

            //For each WineItem in wineItems
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
               
                    //if the wineItem Id is the same as the search id
                    if (beverage.id == _id)
                    {
                    //Set the return string to the result of the wineItem's ToString method
                    Console.WriteLine(header);
                    Console.WriteLine(headed2);
                    returnString = beverage.id.Trim().PadRight(10) + " " + beverage.name.Trim().PadRight(75) + " " +
                                       beverage.pack.Trim().PadRight(20) +
                                       " " + beverage.price.ToString("C").PadRight(10) + " " + beverage.active;
                    }
                
            }
            //Return the returnString
            return returnString;
        }

        public bool UpdateItem(string _id)
        {
            //Bool to hold if item was updated successfully
            bool itemUpdated = false;

            Console.WriteLine("Current wine information");

            //String to hold wineItem information of selected item 
            string itemInfo = FindById(_id);

            if (itemInfo != null)
            {
                //Holds current entery's info in case the update fails
                Beverage tempBeverage = beverageEntities.Beverages.Find(_id);

                //Creates new instance of the beverage to hold new values
                Beverage beverageToUpdate = new Beverage();

                //Sets beverageToUpdate id to the id that is going to be changed
                beverageToUpdate.id = _id;

                //Shows user current beverage information
                Console.WriteLine(tempBeverage.id.Trim().PadRight(10) + " " + tempBeverage.name.Trim().PadRight(75) + " " +
                                  tempBeverage.pack.Trim().PadRight(20) +
                                  " " + tempBeverage.price.ToString("C").PadRight(10) + " " + tempBeverage.active);

                //Get user input for updated information
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Would you like to update the discription? (Y for yes, N for no)");
                Console.Write("> ");
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    Console.WriteLine("Enter new discription");
                    Console.Write("> ");
                    beverageToUpdate.name = Console.ReadLine();
                }
                else
                {
                    beverageToUpdate.name = tempBeverage.name;
                }

                Console.WriteLine();
                Console.WriteLine("Would you like to update the pack? (Y for yes, N for no)");
                Console.Write("> ");
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    Console.WriteLine("Enter new pack");
                    Console.Write("> ");
                    beverageToUpdate.pack = Console.ReadLine();
                }
                else
                {
                    beverageToUpdate.pack = tempBeverage.pack;
                }

                Console.WriteLine();
                Console.WriteLine("Would you like to update the price? (Y for yes, N for no)");
                Console.Write("> ");
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    Console.WriteLine("Enter new price");
                    Console.Write("> ");
                    beverageToUpdate.price = Convert.ToDecimal(Console.ReadLine());
                }
                else
                {
                    beverageToUpdate.price = tempBeverage.price;
                }

                Console.WriteLine();
                Console.WriteLine("Would you like to update if the wine is active? (Y for yes, N for no)");
                Console.Write("> ");
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    Console.WriteLine("Is the wine active? (Y for yes, N for no)");
                    Console.Write("> ");

                    if (Console.ReadLine().Trim().ToUpper() == "Y" )
                    {
                        beverageToUpdate.active = true;
                    }
                }
                else
                {
                    beverageToUpdate.active = tempBeverage.active;
                }

                try
                {
                    //Remove old version of the item
                    beverageEntities.Beverages.Remove(tempBeverage);

                    //Adds Updated version of the beverage
                    beverageEntities.Beverages.Add(beverageToUpdate);

                    //Saves the changes to the database
                    beverageEntities.SaveChanges();

                    //If everything works sets itemUpdated to true
                    itemUpdated = true;

                }
                catch (Exception exception)
                {
                    Console.WriteLine("The item could not be updated. " + exception);

                    
                }
            }
            else
            {
                Console.WriteLine("No item has the id you entered");
            }
            return itemUpdated;
        }

        public bool DeleteItem(string _id)
        {
            //Boolean for if the item is successfully deleted
            bool itemDeleted = false;

            Console.WriteLine("Are you sure you want to delete the following item? (Y for yes, N for no)");

            //String that holds the items info if found
            string itemInfo = FindById(_id);

            if (itemInfo != null)
            {
                //set beverageToDelete info from database
                Beverage beverageToDelete = beverageEntities.Beverages.Find(_id);
                
                Console.WriteLine(beverageToDelete.id.Trim().PadRight(10) + " " + beverageToDelete.name.Trim().PadRight(75) + " " +
                                  beverageToDelete.pack.Trim().PadRight(20) +
                                  " " + beverageToDelete.price.ToString("C").PadRight(10) + " " +
                                  beverageToDelete.active);
                Console.Write("> ");

                //If user inputs Y for yes then item  will be deleted
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    try
                    {
                        //Deletes beverage
                        beverageEntities.Beverages.Remove(beverageToDelete);

                        //Saves state of database
                        beverageEntities.SaveChanges();

                        //Sets deleted bool to true if everything works
                        itemDeleted = true;

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("The item could not be deleted. " + exception);
                    }
                }
            }
            else
            {
                Console.WriteLine("There is not item in the database with that id.");
            }
            return itemDeleted;
        }

    }
}
