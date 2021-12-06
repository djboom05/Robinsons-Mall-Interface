using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Transight.Interface.Common;

namespace TransightInterface
{
    #region old
    //public class posmasterSH
    //{
    //    public decimal salesAmount;
    //    public int totalTransaction;
    //    public DateTime busDate;
    //    public int sequenceNumber;

    //    public decimal getSalesAmount
    //    {
    //        get { return salesAmount; }
    //    }

    //    public int getTotaltransaction
    //    {
    //        get { return totalTransaction; }
    //    }

    //    public DateTime getbusDate
    //    {
    //        get { return busDate;  }
    //    }

    //    public int sequencenumber
    //    {
    //        get { return sequenceNumber; }
    //    }


    //    public posmasterSH(DateTime BusDate, decimal SalesAmount, int TotalTransaction, int SequenceNumber)
    //    {
    //        busDate = BusDate;
    //        salesAmount = SalesAmount;
    //        totalTransaction = TotalTransaction;
    //        sequenceNumber = SequenceNumber;

    //    }

    //}
    #endregion

    #region posmasterSH
    public class posmasterSH
    {

        public decimal salesAmount;
        public int totalTransaction;
        public DateTime busDate;
        public int sequenceNumber;
        //public int sequenceNumber;
        public int fileNo;

        public decimal getSalesAmount
        {
            get { return salesAmount; }
        }

        public int getTotaltransaction
        {
            get { return totalTransaction; }
        }

        public DateTime getbusDate
        {
            get { return busDate; }
        }

        public int sequencenumber
        {
            get { return sequenceNumber; }
        }
        public int getfileNo
        {
            get { return fileNo; }
        }


        public posmasterSH(DateTime BusDate, decimal SalesAmount, int TotalTransaction, int SequenceNumber, int fileNo)
        {
            busDate = BusDate;
            salesAmount = SalesAmount;
            totalTransaction = TotalTransaction;
            sequenceNumber = SequenceNumber;
            this.fileNo = fileNo;
        }

        public posmasterSH()
        {
        }
    }
    #endregion

    public class posmasterSHNew
    {
        //Serangoon--
        //public string machineId;
        public string batchId;
        public DateTime busDate;
        public string hour;
        public string receiptCount;     //Number of transactions receipts issued within the hour
        public decimal gtoSales;        //Nettsales amount after discount and before GST, include service charge for F&B only
        public decimal gst; //sst
        //public decimal discount;
        public decimal srvCharge;
        public string pax;
        public decimal cash;
        public decimal tng;
        public decimal visa;
        public decimal master;
        public decimal amex;
        public decimal voucher;
        public decimal others;
        public string sstReg;

        public string billid;
        public string billdatetime;
        public decimal subtotal;
        public int discpercent;
        public decimal discount;
        public int taxpercent;
        public decimal tax;
        public int scpercent;
        public decimal servicecharge;
        public decimal grandtotal;
 

        public string getBillId
        {
            get { return billid; }
        }

        public string getBillDateTime
        {
            get { return billdatetime; }
        }

        public decimal getSubTotal
        { 
            get { return subtotal; }
        }

        public int getDiscPercent
        { 
            get { return discpercent; } 
        }

        public decimal getDiscount
        {
            get { return discount; }
        }

        public int getTaxPercent
        {
            get { return taxpercent; }
        }

        public decimal getTax
        {
            get { return tax; }
        }

        public int getSCPercent
        {
            get { return scpercent; }
        }

        public decimal getServiceCharge
        {
            get { return servicecharge; }
        }

        public decimal getGrandTotal
        {
            get { return grandtotal; }
        }

        public string getBatchId
        {
            get { return batchId; }
        }

        public DateTime getbusDate
        {
            get { return busDate; }
        }

        public string getHour
        {
            get { return hour; }
        }

        public string getReceiptCount
        {
            get { return receiptCount; }
        }

        public decimal getGtoSales
        {
            get { return gtoSales; }
        }

        public decimal getGst
        {
            get { return gst; }
        }

        //public decimal getDiscount
        //{
        //    get { return discount; }
        //}

        public decimal getSrvCharge
        {
            get { return srvCharge; }
        }

        public string getPax
        {
            get { return pax; }
        }

        public decimal getCash
        {
            get { return cash; }
        }

        public decimal getTng
        {
            get { return tng; }
        }

        public decimal getVisa
        {
            get { return visa; }
        }

        public decimal getMaster
        {
            get { return master; }
        }

        public decimal getAmex
        {
            get { return amex; }
        }

        public decimal getVoucher
        {
            get { return voucher; }
        }

        public decimal getOthers
        {
            get { return others; }
        }

        public string getSstReg
        {
            get { return sstReg; }
        }


        public string TenantID;
        public string TerminalNo;
        public string GrossSales;
        public string TTax;
        public string TVoid;
        public string CVoid;
        public string TDisc;
        public string CDisc;
        public string TRefund;
        public string CRefund;
        public string TNegAdj;
        public string CNegAdj;
        public string TServChg;
        public string PrevZCnt;
        public string PrevGT;
        public string NewZCnt;
        public string NewGT;
        public string BizDate;
        public string Novelty;
        public string Misc;
        public string LocalTax;
        public string TCreditSales;
        public string TCreditTax;
        public string TNVatSales;
        public string Pharma;
        public string NPharma;
        public string TPWDDisc;
        public string GrossNotSub;
        public string TReprint;
        public string CReprint;

        public string getTenantID
        {
            get { return TenantID; }
        }
        public string getTerminalNo
        {
            get { return TerminalNo; }
        }
        public string getGrossSales
        {
            get { return GrossSales; }
        }
        public string getTTax
        {
            get { return TTax; }
        }
        public string getTVoid
        {
            get { return TVoid; }
        }
        public string getCVoid
        {
            get { return CVoid; }
        }
        public string getTDisc
        {
            get { return TDisc; }
        }
        public string getCDisc
        {
            get { return CDisc; }
        }
        public string getTRefund
        {
            get { return TRefund; }
        }
        public string getCRefund
        {
            get { return CRefund; }
        }
        public string getTNegAdj
        {
            get { return TNegAdj; }
        }
        public string getCNegAdj
        {
            get { return CNegAdj; }
        }
        public string getTServChg
        {
            get { return TServChg; }
        }
        public string getPrevZCnt
        {
            get { return PrevZCnt; }
        }
        public string getPrevGT
        {
            get { return PrevGT; }
        }
        public string getNewZCnt
        {
            get { return NewZCnt; }
        }
        public string getNewGT
        {
            get { return NewGT; }
        }
        public string getBizDate
        {
            get { return BizDate; }
        }
        public string getNovelty
        {
            get { return Novelty; }
        }
        public string getMisc
        {
            get { return Misc; }
        }
        public string getLocalTax
        {
            get { return LocalTax; }
        }
        public string getTCreditSales
        {
            get { return TCreditSales; }
        }
        public string getTCreditTax
        {
            get { return TCreditTax; }
        }
        public string getTNVatSales
        {
            get { return TNVatSales; }
        }
        public string getPharma
        {
            get { return Pharma; }
        }
        public string getNPharma
        {
            get { return NPharma; }
        }
        public string getTPWDDisc
        {
            get { return TPWDDisc; }
        }
        public string getGrossNotSub
        {
            get { return GrossNotSub; }
        }
        public string getTReprint
        {
            get { return TReprint; }
        }
        public string getCReprint
        {
            get { return CReprint; }
        }
        

        public posmasterSHNew(string xTenantID, string xTerminalNo, string xGrossSales, string xTTax, string xTVoid, string xCVoid, string xTDisc, string xCDisc, string xTRefund, string xCRefund, string xTNegAdj, string xCNegAdj, string xTServChg, string xPrevZCnt, string xPrevGT, string xNewZCnt, string xNewGT, string xBizDate, string xNovelty, string xMisc, string xLocalTax, string xTCreditSales, string xTCreditTax, string xTNVat, string xPharma, string xNPharma, string xTPWDDisc, string xGrossNotSub, string xTReprint, string xCReprint)
        {
            TenantID = xTenantID;
            TerminalNo = xTerminalNo;
            GrossSales = xGrossSales;
            TTax = xTTax;
            TVoid = xTVoid;
            CVoid = xCVoid;
            TDisc = xTDisc;
            CDisc = xCDisc;
            TRefund = xTRefund;
            CRefund = xCRefund;
            TNegAdj = xTNegAdj;
            CNegAdj = xCNegAdj;
            TServChg = xTServChg;
            PrevZCnt = xPrevZCnt;
            PrevGT = xPrevGT;
            NewZCnt = xNewZCnt;
            NewGT = xNewGT;
            BizDate = xBizDate;
            Novelty = xNovelty;
            Misc = xMisc;
            LocalTax = xLocalTax;
            TCreditSales = xTCreditSales;
            TCreditTax = xTCreditTax;
            TNVatSales = xTNVat;
            Pharma = xPharma;
            NPharma = xNPharma;
            TPWDDisc = xTPWDDisc;
            GrossNotSub = xGrossNotSub;
            TReprint = xTReprint;
            CReprint = xCReprint;
          
        }

        public posmasterSHNew()
        {
        }



      
    }


}
