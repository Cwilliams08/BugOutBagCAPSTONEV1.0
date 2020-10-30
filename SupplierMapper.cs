using DataAccessLayer.DataObjects;
using DataAccessLayer.DataSets;
using IDataAccessLayer;
using Rhino.Mocks.Constraints;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class SupplierMapper : ISupplierMapper
    {
        private string _ConnectionString;

        public SupplierMapper(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        public List<ISupplierDO> ReadAllVendors()
        {
            List<ISupplierDO> Suppliers = new List<ISupplierDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadAllVendors", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                SupplierDataSet DataSet = new SupplierDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "Supplier");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from sql
                    Adapter.Fill(DataSet);

                    //transfer info to lsit of DO
                    Suppliers = (from suppliers in DataSet.Supplier
                                 select new SupplierDO()
                                 {
                                     PKSupplierID = suppliers.PKSupplierID,
                                     FKRoleID = suppliers.FKRoleID,
                                     VendorID = suppliers.VendorID,
                                     PHONENUM = suppliers.PHONENUM,
                                     PASSWORD = suppliers.PASSWORD,
                                     SALT = suppliers.SALT
                                 }).Cast<ISupplierDO>().ToList();
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
                //return list of suppliers
                return Suppliers;
                }
            }

        public void AddSupplier (ISupplierDO SupplierToAdd)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to uise
                SqlCommand Command = new SqlCommand("AddSupplier", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@VendorID", SupplierToAdd.VendorID);
                Command.Parameters.AddWithValue("@FKRoleID", SupplierToAdd.FKRoleID);
                Command.Parameters.AddWithValue("@PHONENUM", SupplierToAdd.PHONENUM);
                Command.Parameters.AddWithValue("@PASSWORD", SupplierToAdd.EncryptedPassword);
                Command.Parameters.AddWithValue("@SALT", SupplierToAdd.SALT);
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

        public void UpdateSupplier(ISupplierDO SupplierToUpdate)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("UpdateUser", Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("@PKSupplierID", SupplierToUpdate.PKSupplierID);
                Command.Parameters.AddWithValue("@VendorID", SupplierToUpdate.VendorID);
                Command.Parameters.AddWithValue("@PHONENUM", SupplierToUpdate.PHONENUM);
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

        public void DeleteSupplier(ISupplierDO SupplierToDelete)
        {
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("DeleteUser" , Connection);
                //set up type of command
                Command.CommandType = CommandType.StoredProcedure;
                //indicate values of parameters
                Command.Parameters.AddWithValue("PKSupplierID", SupplierToDelete.PKSupplierID);
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

        public int ReadSupplierID(string VendorID)
        {
            //creates a class IDo named VendorID to hold vendor info from database
            ISupplierDO SupplierID = new SupplierDO();

           using (SqlConnection Connection = new SqlConnection(_ConnectionString))
           {
               //declare command and initialize which stored procedure to use and which connection
               SqlCommand Command = new SqlCommand("ReadSupplierByVendorID", Connection);
               //indicate which type of command to use
               Command.CommandType = CommandType.StoredProcedure;
               //provide info for any parameters required
               Command.Parameters.AddWithValue("@VendorID", VendorID);
               //declare adapter to be used
               SqlDataAdapter Adapter = new SqlDataAdapter(Command);
               //declare dataset to be used
               SupplierDataSet DataSet = new SupplierDataSet();
               //set up alternate code paths for failed connection
               Adapter.TableMappings.Add("Table", "Supplier");
               try
               {
                   Connection.Open();
                   //fill dataset
                   Adapter.Fill(DataSet);
                   //fill information
                   SupplierID = (from supplierID in DataSet.Supplier
                                 select new SupplierDO()
                                 {
                                     //assign properties for data object to their values
                                     PKSupplierID = supplierID.PKSupplierID
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
           return SupplierID.PKSupplierID;
        }

        public ISupplierDO ReadSupplierByID(int PKSupplierID)
        {
            ISupplierDO Supplier = new SupplierDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadSupplierByID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("PKSuppleirID", PKSupplierID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
                SupplierDataSet DataSet = new SupplierDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "PKSupplierID");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from SQL
                    Adapter.Fill(DataSet);

                    //transfer info to list of DO
                    Supplier = (from suppliers in DataSet.Supplier
                                select new SupplierDO()
                                {
                                    PKSupplierID = suppliers.PKSupplierID,
                                    VendorID = suppliers.VendorID,
                                    PHONENUM = suppliers.PHONENUM,
                                    PASSWORD = suppliers.PASSWORD,
                                    SALT = suppliers.SALT
                                }).Cast<ISupplierDO>().FirstOrDefault();
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
                //return list of suppliers
                return Supplier;
                }
            }

        public ISupplierDO ReadSupplierByVendorID(string VendorIDString)
        {
            ISupplierDO Supplier = new SupplierDO();
            //set up block for conenction to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadSupplierByVendorID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("VendorID", VendorIDString);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from SQL
                SupplierDataSet DataSet = new SupplierDataSet();
                //set up alternate code path for failed connection
                Adapter.TableMappings.Add("Table", "Supplier");
                //set up try catch
                try
                {
                    Connection.Open();
                    //fill dataset with rows from SQL
                    Adapter.Fill(DataSet);
                    //transfer info to list of DO
                    Supplier = (from suppliers in DataSet.Supplier
                                select new SupplierDO()
                                {
                                    PKSupplierID = suppliers.PKSupplierID,
                                    FKRoleID = suppliers.FKRoleID,
                                    VendorID = suppliers.VendorID,
                                    PHONENUM = suppliers.PHONENUM,
                                    PASSWORD = suppliers.PASSWORD,
                                    SALT = suppliers.SALT
                                }).Cast<ISupplierDO>().FirstOrDefault();
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
                //return list of suppliers
                return Supplier;
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
                Command.Parameters.AddWithValue("@PKRoleID", PKRoleID);
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
    }
            }
        
        

    

