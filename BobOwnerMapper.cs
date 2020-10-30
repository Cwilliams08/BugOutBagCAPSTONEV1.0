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
    public class BobOwnerMapper : IBobOwnerMapper
    {
        private string _ConnectionString;

        public BobOwnerMapper(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        public List<IBobOwnerDO> ReadAllBobOwners()
        {
            List<IBobOwnerDO> BobOwners = new List<IBobOwnerDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadAllUsers", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from Sql
                BobOwnerDataSet DataSet = new BobOwnerDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BobOwner");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from Sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BobOwners = (from bobOwners in DataSet.BobOwner
                                 select new BobOwnerDO()
                                 {
                                     PKUserID = bobOwners.PKUserID,
                                     UserName = bobOwners.UserName,
                                     PASSWORD = bobOwners.PASSWORD,
                                     PHONENUM = bobOwners.PHONENUM,
                                     SALT = bobOwners.SALT
                                 }).Cast<IBobOwnerDO>().ToList();
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
                //return list of Bobowners
                return BobOwners;

            }
        }
            
            
                
        public void RegisterBobOwner(IBobOwnerDO BobOwnerToRegister)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("AddUser", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@UserName", BobOwnerToRegister.UserName);
                Command.Parameters.AddWithValue("@FKRoleID", BobOwnerToRegister.FKRoleID);
                Command.Parameters.AddWithValue("@PASSWORD", BobOwnerToRegister.EncryptedPassword);
                Command.Parameters.AddWithValue("@PHONENUM", BobOwnerToRegister.PHONENUM);
                Command.Parameters.AddWithValue("@SALT", BobOwnerToRegister.SALT);
                //set up try catch to respond to errors
                try
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception Exception)
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine(Exception.Message);
                }
                finally
                {
                    Connection.Close();
                }
            
        

            }
        }



        public void AddBobOwner(IBobOwnerDO BobOwnerToAdd)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("AddUser", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@UserName", BobOwnerToAdd.UserName);
                Command.Parameters.AddWithValue("@FKRoleID", BobOwnerToAdd.FKRoleID);
                Command.Parameters.AddWithValue("@PASSWORD", BobOwnerToAdd.EncryptedPassword);
                Command.Parameters.AddWithValue("@PHONENUM", BobOwnerToAdd.PHONENUM);
                Command.Parameters.AddWithValue("@SALT", BobOwnerToAdd.SALT);
                //set up try catch to respond to errors
                try
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception Exception)
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine(Exception.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public void UpdateBobOwner(IBobOwnerDO BobOwnerToUpdate)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("UpdateUser", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@PKUserID", BobOwnerToUpdate.PKUserID);
                Command.Parameters.AddWithValue("@FKRoleID", BobOwnerToUpdate.FKRoleID);
                Command.Parameters.AddWithValue("@UserName", BobOwnerToUpdate.UserName);
                Command.Parameters.AddWithValue("@PHONENUM", BobOwnerToUpdate.PHONENUM);
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

        public void DeleteBobOwner(IBobOwnerDO BobOwnerToDelete)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("DeleteUser", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("PKUserID", BobOwnerToDelete.PKUserID);
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

        public IBobOwnerDO ReadBobOwnerByID(int PKUserID)
        {
            IBobOwnerDO BobOwner = new BobOwnerDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadUserByID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@PKUserID", PKUserID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from Sql
                BobOwnerDataSet DataSet = new BobOwnerDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "PKUserID");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    BobOwner = (from bobOwners in DataSet.BobOwner
                                select new BobOwnerDO()
                    {
                        PKUserID = bobOwners.PKUserID,
                        UserName = bobOwners.UserName,
                        PASSWORD = bobOwners.PASSWORD,
                        PHONENUM = bobOwners.PHONENUM,
                        SALT = bobOwners.SALT
                    }).Cast<IBobOwnerDO>().FirstOrDefault();
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
                //return list of users
                return BobOwner;
                    }
                }

        public IBobOwnerDO ReadBobOwnerByUserName(string UserNameString)
        {
            IBobOwnerDO BobOwner = new BobOwnerDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadUserByUserName", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("UserName", UserNameString);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                BobOwnerDataSet DataSet = new BobOwnerDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "BobOwner");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from SQL
                    Adapter.Fill(DataSet);
                    //transfer info to list of DO
                    BobOwner = (from bobOwners in DataSet.BobOwner
                                select new BobOwnerDO()
                                {
                                    PKUserID = bobOwners.PKUserID,
                                    FKRoleID = bobOwners.FKRoleID,
                                    UserName = bobOwners.UserName,
                                    PASSWORD = bobOwners.PASSWORD,
                                    PHONENUM = bobOwners.PHONENUM,
                                    SALT = bobOwners.SALT
                    }).Cast<IBobOwnerDO>().FirstOrDefault();
                }
                catch (SqlException Exception)
                {
                    Console.WriteLine(Exception.Message);
                }
                finally{
                    //close connection
                    Connection.Close();
                }
                //return list of BobOwners
                return BobOwner;
                                }
                }

        public List<IRoleDO> ReadAllUserRoles()
        {
            List<IRoleDO> UserRoles = new List<IRoleDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored proedure to use
                SqlCommand Command = new SqlCommand("ReadAllUserRoles", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from Sql
                RoleDataSet DataSet = new RoleDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "Role");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from Sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    UserRoles = (from userRoles in DataSet.Role
                                 select new RoleDO()
                                 {
                                     PKRoleID = userRoles.PKRoleID,
                                     ROLE = userRoles.ROLE,
                                 }).Cast<IRoleDO>().ToList();
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
                //return list of Bobowners
                return UserRoles;

            }
        }
        

        public void SearchBobOwner(IBobOwnerDO BobOwnerToSearch)
        {
            throw new NotImplementedException();
        }





        public IRoleDO ReadRoleByID(int PKRoleID)
        {
            IRoleDO RoleID = new RoleDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadRoleByID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@PKRoleID",PKRoleID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from Sql
                RoleDataSet DataSet = new RoleDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "Role");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                   RoleID = (from roles in DataSet.Role
                                select new RoleDO()
                                {
                                    PKRoleID = roles.PKRoleID,
                                    ROLE = roles.ROLE
                                }).Cast<IRoleDO>().FirstOrDefault();
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
                //return list of roles
                return RoleID;
            }
        }

        public int ReadUserID(string UserName)
        {
            //creates a class IDo named VendorID to hold vendor info from database
            IBobOwnerDO UserID = new BobOwnerDO();

            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //declare command and initialize which stored procedure to use and which connection
                SqlCommand Command = new SqlCommand("ReadUserByUserName", Connection);
                //indicate which type of command to use
                Command.CommandType = CommandType.StoredProcedure;
                //provide info for any parameters required
                Command.Parameters.AddWithValue("@UserName", UserName);
                //declare adapter to be used
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //declare dataset to be used
                BobOwnerDataSet DataSet = new BobOwnerDataSet();
                //set up alternate code paths for failed connection
                Adapter.TableMappings.Add("Table", "BobOwner");
                try
                {
                    Connection.Open();
                    //fill dataset
                    Adapter.Fill(DataSet);
                    //fill information
                    UserID = (from userID in DataSet.BobOwner
                                  select new BobOwnerDO()
                                  {
                                      //assign properties for data object to their values
                                      PKUserID = userID.PKUserID
                                  }).FirstOrDefault();
                }
                catch (SqlException Exception)
                {
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine(Exception.Message);
                }
                finally
                {
                    //close database connection
                    Connection.Close();
                }
            }
            //return dataobject to the business layer
            return UserID.PKUserID;
        }

        public IBobOwnerDO ReadBobOwnerByUserID(int PKUserID)
        {
            throw new NotImplementedException();
        }
    }
        }


