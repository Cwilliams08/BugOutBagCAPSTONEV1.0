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
    public class BugOutBagMapper : IBugOutBagMapper
    {
        private string _ConnectionString;

        public BugOutBagMapper(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }
        public List<IBugOutBagDO> ReadAllBugOutBags()
        {
            List<IBugOutBagDO> BugOutBags = new List<IBugOutBagDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadAllBugOutBags", Connection);
                //define type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                BugOutBagDataSet DataSet = new BugOutBagDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BugOutBag");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BugOutBags = (from bugoutbags in DataSet.BugOutBag
                                  select new BugOutBagDO()
                                  {
                                      PKBugOutBagID = bugoutbags.PKBugOutBagID,
                                      FKUserID = bugoutbags.FKUserID,
                                      BagName = bugoutbags.BagName,
                                      TotalCost = bugoutbags.TotalCost,
                                      TotalSurvRate = bugoutbags.TotalSurvRate
                                  }).Cast<IBugOutBagDO>().ToList();
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
                //return list of BugOutBags
                return BugOutBags;
            }
        }

        public IBugOutBagDO ReadBugOutBagByID(int PKBugOutBagID)
        {
            IBugOutBagDO BugOutBag = new BugOutBagDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadBugOutBagByID", Connection);
                //define type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("PKBugOutBagID", PKBugOutBagID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                BugOutBagDataSet DataSet = new BugOutBagDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BugOutBag");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BugOutBag = (from bugoutbags in DataSet.BugOutBag
                                     select new BugOutBagDO()
                                     {
                                      PKBugOutBagID = bugoutbags.PKBugOutBagID,
                                      FKUserID = bugoutbags.FKUserID,
                                      BagName= bugoutbags.BagName,
                                      TotalCost = bugoutbags.TotalCost,
                                      TotalSurvRate = bugoutbags.TotalSurvRate
                                  }).Cast<IBugOutBagDO>().FirstOrDefault();
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
                //return list of bugoutbags
                return BugOutBag;
                }
            }

        public void DeleteBugOutBag(IBugOutBagDO BugOutBagToDelete)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("DeleteBugOutBag", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("PKBugOutBagID", BugOutBagToDelete.PKBugOutBagID);
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

        public void UpdateBugOutBag(IBugOutBagDO BugOutBagToUpdate)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("UpdateBugOutBag", Connection);
                //set up type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("PKBugOutBagID", BugOutBagToUpdate.PKBugOutBagID);
                Command.Parameters.AddWithValue("BagName", BugOutBagToUpdate.BagName);
                //set up try catch to respond to error
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

        public IBugOutBagDO ViewBugOutBag(string BagName)
        {
            IBugOutBagDO BugOutBag = new BugOutBagDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ViewBugOutBag", Connection);
                //define type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("BagName", Connection);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                BugOutBagDataSet DataSet = new BugOutBagDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BugOutBag");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BugOutBag = (from bugoutbags in DataSet.BugOutBag
                                 select new BugOutBagDO()
                                 {
                                     PKBugOutBagID = bugoutbags.PKBugOutBagID,
                                     FKUserID = bugoutbags.FKUserID,
                                     BagName = bugoutbags.BagName,
                                     TotalCost = bugoutbags.TotalCost,
                                     TotalSurvRate = bugoutbags.TotalSurvRate
                                 }).Cast<IBugOutBagDO>().FirstOrDefault();
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
                //return list of bugoutbags
                return BugOutBag;
            }
        }
        
        public void AddBugOutBag(IBugOutBagDO BugOutBagToAdd)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                SqlCommand Command = new SqlCommand("AddBugOutBag", Connection);
                //set up type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@BagName", BugOutBagToAdd.BagName);
                Command.Parameters.AddWithValue("@FKUserID", BugOutBagToAdd.FKUserID);
                Command.Parameters.AddWithValue("@TotalCost", BugOutBagToAdd.TotalCost);
                Command.Parameters.AddWithValue("@TotalSurvRate", BugOutBagToAdd.TotalSurvRate);
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



        public void ViewBagList(IBugOutBagDO BugOutBagViewList)
        {
                        IBugOutBagDO BugOutBag = new BugOutBagDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadAllBugOutBags", Connection);
                //define type of command
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("BagName", "BugOutBag");
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                BugOutBagDataSet DataSet = new BugOutBagDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BugOutBag");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BugOutBag = (from bugoutbags in DataSet.BugOutBag
                                 select new BugOutBagDO()
                                 {
                                     PKBugOutBagID = bugoutbags.PKBugOutBagID,
                                     FKUserID = bugoutbags.FKUserID,
                                     BagName = bugoutbags.BagName,
                                     TotalCost = bugoutbags.TotalCost,
                                     TotalSurvRate = bugoutbags.TotalSurvRate
                                 }).Cast<IBugOutBagDO>().FirstOrDefault();
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
                //return list of bugoutbags
                return;
            }
        }


        public List<IBagContentsDO> ReadContentsByBagID(int PKBugOutBagID)
        {
            List<IBagContentsDO> BugOutBagContents = new List<IBagContentsDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadBagContentsByBagID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@FKBugOutBagID", PKBugOutBagID);
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
                    BugOutBagContents = (from bagcontents in DataSet.BagContents
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
                return BugOutBagContents;
                                     }
                }


    }
        }
    
    

    

