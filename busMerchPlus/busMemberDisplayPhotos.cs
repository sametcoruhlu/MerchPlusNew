using datMerchPlus;
using entMerchPlus;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busMerchPlus
{
    public class busMemberDisplayPhotos : BusBase
    {
        public busMemberDisplayPhotos() : base()
        {
        }

        /// <summary>
        /// Inserts one row of data into table [Area] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberDisplayPhotos">Gets entity object as parameter for table Area]</param>
        public void InsertMemberDisplayPhotos(entMemberDisplayPhotos parEntMemberDisplayPhotos)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDisplayPhotos insDatMemberDisplayPhotos = new datMemberDisplayPhotos();
                insDatMemberDisplayPhotos.InsertMemberDisplayPhotos(parEntMemberDisplayPhotos, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }
    }
}
