using DataAccessLayer.DataObjects;
using DataAccessLayer.DataSets;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class ItemMapper : IItemMapper
    {
        private string _ConnectionString;

        public ItemMapper(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }
    public List<IItemDO> ReadAllItems()
{
    List<IItemDO> Items = new List<IItemDO>();
    //set up block for connection to database
        using (SqlConnection Connection = new SqlConnection(_ConnectionString))
        {
            //set up command and indicate which stored procedure to use
            SqlCommand Command = new SqlCommand("ReadAllItems", Connection);
            //define type of command
            Command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Adapter = new SqlDataAdapter(Command);
            //create data set to hold info from sql
            ItemDataSet DataSet = new ItemDataSet();
            //set up alternate code path for failed connection
            Adapter.TableMappings.Add("Table", "Items");
            //set up try catch
            try
            {
                Connection.Open();
                //fill dataset with rows from sql
                Adapter.Fill(DataSet);

                //transfer info to list of DO
                Items = (from items in DataSet.Items
                             select new ItemDO()
                             {
                                 PKItemID = items.PKItemID,
                                 FKSupplierID = items.FKSupplierID,
                                     ItemName = items.ItemName,
                                     Details = items.Details,
                                     Cost = items.Cost,
                                     SurvRate = items.SurvRate
                                 }).Cast<IItemDO>().ToList();
            }
            catch (SqlException Exception)
            {
                Console.WriteLine(Exception.Message);
            }
            finally{
                //close connection
                Connection.Close();
            }
            //return list of items
            return Items;
                             }
            }
        


    public void AddItem(IItemDO ItemToAdd)
{
    using (SqlConnection Connection = new SqlConnection(_ConnectionString))
{
         SqlCommand Command = new SqlCommand("AddItem", Connection);
         //set up type of command
         Command.CommandType = CommandType.StoredProcedure;
         //indicate values of parameters
         Command.Parameters.AddWithValue("@ItemName", ItemToAdd.ItemName);
         Command.Parameters.AddWithValue("@FKSupplierID", ItemToAdd.FKSupplierID);
         Command.Parameters.AddWithValue("@Details", ItemToAdd.Details);
         Command.Parameters.AddWithValue("@Cost", ItemToAdd.Cost);
         Command.Parameters.AddWithValue("@SurvRate", ItemToAdd.SurvRate);
                                          //PKItemID = items.PKItemID,
                                          //FKSupplierID = items.FKSupplierID,
                                          //ItemName = items.ItemName,
                                          //Details = items.Details,
                                          //Cost = items.Cost,
                                          //SurvRate = items.SurvRate
         //set up try catch to respond to errors
         try
         {
             Connection.Open();
             Command.ExecuteNonQuery();
         }
         catch (Exception Exception)
         {
             Console.WriteLine("Invalid Input");
             Console.WriteLine(Exception.Message);
         }
         finally
         {
             Connection.Close();
         }
    }
    }

        public void UpdateItem(IItemDO ItemToUpdate)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("UpdateItem", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@PKItemID", ItemToUpdate.PKItemID);
                Command.Parameters.AddWithValue("@ItemName", ItemToUpdate.ItemName);
                Command.Parameters.AddWithValue("@FKSupplierID", ItemToUpdate.FKSupplierID);
                Command.Parameters.AddWithValue("@Details", ItemToUpdate.Details);
                Command.Parameters.AddWithValue("@Cost", ItemToUpdate.Cost);
                Command.Parameters.AddWithValue("@SurvRate", ItemToUpdate.SurvRate);
                //set up try catch to respond to errors
                try
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public void DeleteItem(IItemDO ItemToDelete)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("DeleteItem", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("PKItemID", ItemToDelete.PKItemID);
                //set up try catch to respond to errors
                try{
                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception Exception)
                {
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine(Exception.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public IItemDO ReadItemByID(int PKItemID)
        {
            IItemDO Item = new ItemDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadItemByID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("PKItemID", PKItemID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from Sql
                ItemDataSet DataSet = new ItemDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "Items");
                //set up try catch
                try{
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    Item = (from items in DataSet.Items
                                select new ItemDO()
                                {
                                    PKItemID = items.PKItemID,
                                    FKSupplierID = items.FKSupplierID,
                                    ItemName = items.ItemName,
                                    Details = items.Details,
                                    Cost = items.Cost,
                                    SurvRate = items.SurvRate
                                }).Cast<IItemDO>().FirstOrDefault();
                }
                catch (SqlException Exception)
                {
                    Console.WriteLine(Exception.Message);
                }
                finally
                {
                    //close connection
                    Connection.Close();
                }
                //return list of items
                return Item;
            }
        }
        public IItemDO ReadItemByItemName(string ItemNameString)
        {
            IItemDO Item = new ItemDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadItemByItemname", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("Itemname", ItemNameString);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                ItemDataSet DataSet = new ItemDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "Item");
                //set up try catch
                try{
                    Connection.Open();
                    //fill dataset with rows from Sql
                    Adapter.Fill(DataSet);
                    //transfer info to list of DO
                    Item = (from items in DataSet.Items
                            select new ItemDO()
                            {
                                    PKItemID = items.PKItemID,
                                    FKSupplierID = items.FKSupplierID,
                                    ItemName = items.ItemName,
                                    Details = items.Details,
                                    Cost = items.Cost,
                                    SurvRate = items.SurvRate
                                }).Cast<IItemDO>().FirstOrDefault();
                }
                catch (SqlException Exception)
                {
                    Console.WriteLine(Exception.Message);
                }
                finally{
                    //close connection
                    Connection.Close();
                }
                //return list of Items
                return Item;
                            }
                }
            }
        }

