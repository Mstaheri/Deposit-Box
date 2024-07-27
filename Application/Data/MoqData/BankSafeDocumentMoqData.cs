using Domain.Entity;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.MoqData
{
    public class BankSafeDocumentMoqData
    {
        public Task<BankSafeDocument> Get()
        {
            BankSafeDocument bankSafeDocument = new BankSafeDocument("ali",
                "1234123412341234" , "1403/02/03" , "1403/06/05", 1122,
                0 , SituationTypes.Confirmed);

            return Task.FromResult(bankSafeDocument);
        }
        public Task<List<BankSafeDocument>> GetAll()
        {
            List<BankSafeDocument> list = new List<BankSafeDocument>()
            {
                new BankSafeDocument("ali",
                "1234123412341234" , "1403/02/03" , "1403/06/05", 1122,
                1 , SituationTypes.UnderReview) ,
                new BankSafeDocument("ali",
                "1234123412341234" , "1403/02/03" , "1403/06/05",0,
                21432 , SituationTypes.Returned)
            };
            return Task.FromResult(list);
        }
    }
}
