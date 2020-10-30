using DataAccessLayer;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class MapperFactory
    {
        /// <summary>
        /// Generates instance of a BobOwner mapper to pass into the call
        /// to the BLO from the controller
        /// </summary>
        /// <param name="iConnectionString"></param>
        /// <returns></returns>


        public IBobOwnerMapper GenerateBobOwnerMapper(string iConnectionString)
        {
            //create instance of mapper and camoflauge as Imapper
            IBobOwnerMapper Mapper = new BobOwnerMapper(iConnectionString);
            //return back as disguised mapper
            return Mapper;
        }

        public IRoleMapper GenerateRoleMapper(string _ConnectionString)
        {
            //create instance of mapper and camo as Imapper
            IRoleMapper Mapper = new RoleMapper(_ConnectionString);
            //return back as disguised mapper
            return Mapper;
        }

        public IBagContentsMapper GenerateBagContentsMapper(string _ConnectionString)
        {
            //create instance of mapper and camo as Imapper
            IBagContentsMapper Mapper = new BagContentsMapper(_ConnectionString);
            //return back as disguised mapper
            return Mapper;
        }

        public IBugOutBagMapper GenerateBugOutBagMapper(string _ConnectionString)
        {
            //create instance of mapper and camo as Imapper
            IBugOutBagMapper Mapper = new BugOutBagMapper(_ConnectionString);
            //return back as disguised mapper
            return Mapper;
        }

        public IItemMapper GenerateItemMapper(string _ConnectionString)
        {
            //create instance of mapper and camo as Imapper
            IItemMapper Mapper = new ItemMapper(_ConnectionString);
            //reutrn back as disguised mapper
            return Mapper;
        }




        public ISupplierMapper GenerateSupplierMapper(string _ConnectionString)
        {
            //create instance of mapper and camo as Imapper
            ISupplierMapper Mapper = new SupplierMapper(_ConnectionString);
            //return bac k as disguised mapper
            return Mapper;
        }
    }
    }

