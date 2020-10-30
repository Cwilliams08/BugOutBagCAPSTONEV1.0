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
    public class RoleMapper : IRoleMapper
    {
        private string _ConnectionString;

        public RoleMapper(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        public List<IRoleDO> ReadAllRoles()
        {
            List<IRoleDO> Roles = new List<IRoleDO>();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadAllRoles", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
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
                    Roles = (from roles in DataSet.Role
                             select new RoleDO()
                             {
                                 PKRoleID = roles.PKRoleID,
                                 ROLE = roles.ROLE
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
                //return list of roles
                return Roles;
                }

            }

        public IRoleDO ReadRoleByID(int RoleID)
        {
            IRoleDO Role = new RoleDO();
            //set up block for connection to database
            using (SqlConnection Connection = new SqlConnection(_ConnectionString))
            {
                //set up command and indicate which stored procedure to use
                SqlCommand Command = new SqlCommand("ReadRoleByID", Connection);
                //define type of command
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("Role", RoleID);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                //create data set to hold info from sql
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
                    Role = (from roles in DataSet.Role
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
                return Role;           
                
            }
        }

        }
    }

