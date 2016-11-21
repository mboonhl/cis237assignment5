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
    class Program
    {
        
        static void Main(string[] args)
        {
            //Change Height and width of console to fit everything
            Console.SetWindowSize(200, 50);
            
            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Create an instance of the RepositoryAPI
            RepositoryAPI repositoryApi = new RepositoryAPI();

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                       userInterface.DisplayImportSuccess();
                        break;

                    case 2:
                        //Print Entire List Of Items
                        string[] allItems = repositoryApi.GetPrintStringsForAllItems();
                        if (allItems.Length > 0)
                        {
                            //Display all of the items
                            userInterface.DisplayAllItems(allItems);
                        }
                        else
                        {
                            //Display error message for all items
                            userInterface.DisplayAllItemsError();
                        }
                        break;

                    case 3:
                        //Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();
                        string itemInformation = repositoryApi.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 4:
                        //Add A New Item To The List

                        string[] newItem = userInterface.GetNewItemInformation();

                        decimal price = Convert.ToDecimal(newItem[3]);

                        //Boolean to hold if item is active
                        bool active = false;

                        //If the user inputs Y for active the active boolean is set to true
                        if (newItem[4] == "Y")
                        {
                            active = true;
                        }

                        //Send information to RepositoryAPI to add new item
                        if (repositoryApi.AddNewItem(newItem[0], newItem[1], newItem[2], price, active))
                        {
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;

                    case 5:

                        //Update item
                        string idToUpdate = userInterface.GetIdToUpdate();

                        if (repositoryApi.UpdateItem(idToUpdate))
                        {
                            userInterface.DisplayItemUpdateSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemUpdateFailure();
                        }
                        break;

                    case 6:
                        //Delete item
                        string idToDelete = userInterface.GetIdToDelete();

                        if (repositoryApi.DeleteItem(idToDelete))
                        {
                            userInterface.DisplayItemDeleteSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemDeleteFailure();
                        }
                        break;
                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }

        }
    }
}
