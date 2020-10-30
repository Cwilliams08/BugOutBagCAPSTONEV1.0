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
    public class BagContentsMapper : IBagContentsMapper
    {
        private string _ConnectionString;

        public BagContentsMapper(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }
        public List<IBagContentsDO> ReadAllBagContents()
        {
            List<IBagContentsDO> BagContents = new List<IBagContentsDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadAllBagContents", Connection);
                //define type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                BagContentsDataSet DataSet = new BagContentsDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BagContents");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BagContents = (from bagcontents in DataSet.BagContents
                                   select new BagContentsDO()
                                   {
                                       PKBagContentsID = bagcontents.PKBagContentsID,
                                       FKBugOutBagID = bagcontents.FKBugOutBagID,
                                       FKItemID = bagcontents.FKItemID
                                   }).Cast<IBagContentsDO>().ToList();
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
                //return list of BagContents
                return BagContents;
            }
        }


        public void AddContentsToBugOutBag(IBagContentsDO AddContentsToBugOutBag)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and in dicate which stored procedure to use
                SqlCommand Command = new SqlCommand("AddContentsToBugOutBag", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@FKItemID", AddContentsToBugOutBag.FKItemID);
                Command.Parameters.AddWithValue("@FKBugOutBagID", AddContentsToBugOutBag.FKBugOutBagID);
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


        public void DeleteContentsFromBugOutBag(IBagContentsDO DeleteContentsFromBugOutBag)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("DeleteContentsFromBugOutBag", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("PKBagContentsID", DeleteContentsFromBugOutBag.PKBagContentsID);
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
                finally
                {
                    //close connection
                    Connection.Close();
                }
                //return list of items
                return Items;
            }
        }



        public List<IBagContentsDO> GetItemPerContents(int PKItemID)
        {
            List<IBagContentsDO> GetItemPerContents = new List<IBagContentsDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("GetItemPerContents", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@FKItemID", PKItemID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create dataset to hold info from sql
                BagContentsDataSet DataSet = new BagContentsDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BagContents");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);
                    //transfer info to list of DO
                    GetItemPerContents = (from bagcontents in DataSet.BagContents
                                         select new BagContentsDO()
                                         {
                                             PKBagContentsID = bagcontents.PKBagContentsID,
                                             FKBugOutBagID = bagcontents.FKBugOutBagID,
                                             FKItemID = bagcontents.FKItemID
                                         }).Cast<IBagContentsDO>().ToList();
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
                //return list of bug out bags
                return GetItemPerContents;
            }
        }


        
    }

    }

