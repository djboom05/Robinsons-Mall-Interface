USE [POS]
GO
/****** Object:  StoredProcedure [dbo].[sp_SalesDailyRobMallTxtfile]    Script Date: 03/01/2022 12:37:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_SalesDailyRobMallTxtfile]
@spDate datetime,
@spPCSEQ int,
@spFirstRead datetime
AS 
BEGIN
------------------------------------------------------------------------------------------------------------------------------
-- DATE			NAME			DESCRIPTION
-- 20 FEB 2019	Lee Chye Hwee	Initial Draft
-- 01 Jul 2019  Thong Chin Sien export daily sales to tenant mall. Mont Kiara
-- 10 Jul 2019  Thong Chin Sien Exclude Tips from net amount
------------------------------------------------------------------------------------------------------------------------------


Declare @sSDate as datetime
Declare @sFirstZRead as datetime
Declare @dGrandTtl1 Decimal(18,2)
Declare @dGrandTtl2 Decimal(18,2)
--Declare @pcnum int

--set @pcnum = @spPCSEQ /*param for update */

set @sSDate = @spDate /*param for update */
set @sFirstZRead = @spFirstRead /*param for update */
set @dGrandTtl1 = isnull((select SUM(grandtotal) from grandttl where busidate = @sSDate),0)
set @dGrandTtl2 = isnull((select SUM(grandtotal) from grandttl where busidate = DATEADD(day,-1,@sSDate )),0)
--set @dGrandTtl1 = isnull((select SUM(grandtotal + grandtotal_tax1) from pc_grandttl where pc = @pcnum AND busidate = @sSDate),0)
--set @dGrandTtl2 = isnull((select SUM(grandtotal + grandtotal_tax1) from pc_grandttl where pc = @pcnum AND busidate = DATEADD(day,-1,@sSDate )),0)

select  
--(select number from pc where id =@pcnum) as TerminalNo,
'1' as TerminalNo,
CAST(isnull(sttl.Netsales,0) + Abs(isnull(vv.dj,0)*1.12) + 
Abs(isnull(addd.adds,0))+ Abs(isnull(jk.kl,0)) + 
Abs(isnull(sttl.Tax1,0)) as decimal(10,2)) as Gross, /*03*/
 CAST(isnull(isnull(sttl.Netsales,0) + Abs(isnull(vv.dj,0)*1.12) +
Abs(isnull(addd.adds,0))+ Abs(isnull(jk.kl,0)) + 
Abs(isnull(sttl.Tax1,0)) - (abs(isnull(addd.adds, 0)) + 
abs(isnull(jk.kl,0))) /.2*.8 - Abs(isnull(jk.kl,0)) - 
Abs(isnull(addd.adds,0)),0)/1.12*0.12 as decimal(10,2)) as Tax, 
  CAST(Abs(isnull(sttl.Void,0)) as decimal(10,2)) as TVoid,
  Abs(isnull(sttl.CVoid,0)) as CVoid,
  CAST(Abs(isnull(vv.dj,0))*1.12 as decimal(10,2)) as TDisc,  /*7*/
 
  isnull(ass.abi,0) as CDisc, 
  
  CAST(Abs(isnull(sttl.TReturn + sttl.TOverring,0)) as decimal(10,2)) as TRefund, 
  Abs(isnull(sttl.CReturn + chkttl.COverring,0)) as CRefund, 
  CAST(Abs(isnull(addd.adds,0)) as decimal(10,2)) as TNegDisc,/*11*/ 
  Abs(isnull(bb.dd,0)) as CNegDisc, /*12*/
  CAST(Abs(isnull(sttl.Autosvc,0)) as decimal(10,2)) as Svchg, 
  
  isnull(pc.cpc,0) as OZCnt,
    CAST(Abs(isnull(@dGrandTtl2,0)-isnull(d.Autosvc1,0)) as decimal(10,2)) as OGT,

  Abs(isnull(pc.cpc + 1,0)) as ZCnt,
  CAST(isnull(isnull(isnull(isnull(isnull(sttl.Netsales,0) + 
	Abs(isnull(vv.dj,0)*1.12) + 
	Abs(isnull(addd.adds,0)) + 
	Abs(isnull(jk.kl,0)) + 
	Abs(isnull(sttl.Tax1,0)) + 
	Abs(isnull(sttl.Servchg,0)) + 
	isnull(sttl.ttx,0),0)-
	Abs(isnull(vv.dj,0)*1.12),0)- 
	Abs(isnull(addd.adds,0)),0)- 
	Abs(isnull(jk.kl,0)),0)+
	isnull(@dGrandTtl2,0)-isnull(d.Autosvc1,0) as decimal(10,2)) as NGT,
	CONVERT(NVARCHAR(10),@sSDate,101) as [Date],
	CAST(0 as decimal(10,2)) as Novelty,
	CAST(0 as decimal(10,2)) as Misc,
	CAST( isnull(sttl.Taxttl,0) - isnull(sttl.Tax1,0) as decimal(10,2)) as LocalTax, 

  CAST(Abs(isnull(tttl2.Chrgttl,0)) as decimal(10,2)) as TChrg,

   CAST(Abs(isnull(tttl2.ChrgTtl,0))/1.12*0.12 as decimal(10,2)) as TChrgTax,
  --CAST(0 as decimal(10,2)) as TChrgTax,
  
  CAST((abs(isnull(addd.adds, 0)) + abs(isnull(jk.kl,0))) /.2*.8 as decimal(10,2)) as NTaxSales,
  CAST(0 as decimal(10,2)) as PharmaSales,
  CAST(0 as decimal(10,2)) as NPharmaSales,
  CAST(isnull(jk.kl,0) as decimal(10,2)) as TPWDDisc,/*27*/

  CAST(0 as decimal(10,2)) as GSnotSubjectRent, /*28*/
  CAST(isnull(TAmtReprint.amt,0) as decimal(10,2)) as ReprintAmt,
  isnull(TCntReprint.cnt,0) as ReprintCnt
    from 
    (select sum(netsales) as NetSales, 
      sum(tservchg) as Servchg, 
      sum(tautosvc + tservchg) as Autosvc, 
      sum(tdiscount) as TtlDisc, 
      sum(cdiscount) as CDisc, 
      sum(tMGRVOID)  as Void, 
      sum(TCancel) as TCancel,
      sum(CCancel) as CCancel,
      sum(treturn) as TReturn, 
      sum(creturn) as CReturn, 
      sum(vattax1) + sum(tax1) as Vattax1,
      sum(tax1 + tax2 + tax3 + tax4 + tax5 + tax6 + tax7 + tax8) as Taxttl, 
      sum(tax1) as Tax1,
      sum(tax2 + tax3 ) as ttx,
      sum(cmgrvoid) as CVoid,
      sum(toverring) as TOverring,
      sum(nontaxablesales) as nontax
      from salesttl st where st.busidate = @sSDate ) sttl,


    (select sum(coverring) as COverring   
      from checkttl chkttl where chkttl.busidate = @sSDate ) chkttl,
(select sum(tautosvc) as Autosvc1 from salesttl where busidate <= DATEADD(day,-1,@sSDate)) d,

    (select sum(d.chk_ttl) as arr from details d, checkttl cc where d.dtl_type='D' and cc.busidate = @sSDate ) rar,
    (select sum(d.chk_ttl) as dads from details d, checkttl cc where d.number<>'101'  and d.dtl_type='M' or d.dtl_type='D'and cc.busidate = @sSDate ) mams,
    (select max(chk_ttl)/1.12*0.12 as freya from details c, transactions t where c.chk_seq=t.chk_seq and sales_type_seq=85 and t.business_date=@sSDate ) eya,
    (select sum(c.pymnt_ttl) as lala from checks c,checkttl ct where c.chk_prntd_cnt > '1' and ct.busidate=@sSDate) rare,
(select sum(c.chk_prntd_cnt)/2 as cnt 
      from checks as c, transactions as ct 
      where c.chk_clsd_date_time = ct.end_time
      and c.chk_prntd_cnt >'1' and c.pymnt_ttl >'0' and ct.business_date = @sSDate) TCntReprint, /*line #30*/
(select sum(c.pymnt_ttl) as amt 
      from checks as c, transactions as ct 
      where c.chk_clsd_date_time = ct.end_time
      and c.chk_prntd_cnt >'1' and c.pymnt_ttl > '0'and ct.business_date = @sSDate) TAmtReprint,	 /*line #29*/
 (select sum(gt.grandtotal) as grandttl  
      from grandttl gt where gt.busidate = @sSDate ) gttl,

    (select sum(tt1.total) as CashTtl
      from tenderttl tt1 
      where tt1.busidate = @sSDate and id = (select id from tender where number = 10)) tttl1,
    (select sum(tt2.total) as ChrgTtl
      from tenderttl tt2 
      where tt2.busidate = @sSDate and id in (select id from tender where number   in (16))) tttl2, 
    (select Count(tt1.total) as CashCnt
      from tenderttl tt1 
      where tt1.busidate = @sSDate and id = (select id from tender where number = 10)) tttl3,
    (select Count(tt2.total) as ChrgCnt
      from tenderttl tt2 
      where tt2.busidate = @sSDate and id in (select id from tender where number   in (16))) tttl4,
	(select min(c.chk_num) as MinNo , 
      max(c.chk_num) as MaxNo, 
      min(c.chk_open_date_time) as MinChkOpenDateTime, 
      max(c.chk_open_date_time) as MaxChkOpenDateTime,
      sum(t.cov_cnt) as CoverCount
      from transactions t, checks c 
      where c.chk_seq = t.chk_seq and  t.business_date = @sSDate) tc,
    (select sum([begin]) as TransCnt
      from checkttl ct where ct.busidate = @sSDate) cttl,
  (select sum([count]) as dd from discttl 
      where id in (select id from discount 
      where number in (101)) and busidate = @sSDate)bb,   /*line #12*/

   (select sum([count]) as abi from discttl
      where id in (select id from discount 
      where number not in (101,102,117,300,301)) and busidate = @sSDate)ass, /*line #8*/ 

   (select round(sum(([total])),2) as dj from discttl 
      where id in (select id from discount 
      where number not in (101,102,117,300,301)) and busidate = @sSDate) vv, /*line #7*/

   (select sum([total]) as adds from discttl 
      where id = (select id from discount 
      where number = (101)) and busidate = @sSDate)addd,   /*line #11*/

   (select abs(round(sum(([total])),1)) as kl from discttl 
      where id in (select id from discount 
      where number = (102)) and busidate = @sSDate)jk, /*line 27*/
   (select sum(nosale) as NoSales
      from nsplttl nt where nt.busidate = @sSDate ) nttl,
    
   (select sum(d1.rpt_ttl) as nontaxablesales 
      from details d1, transactions t1 
      where d1.chk_seq = t1.chk_seq and
      ((rpt_cnt <> 0 or charindex(substring(d1.status, 4, 1), '4567CDEF') = 0) and 
      charindex(substring(d1.status, 5, 1), '2367ABEF') = 0) and taxenable = '00' and 
      charindex(substring(d1.status, 3, 1), '13579BDF') = 0 and 
      dtl_type in ('M') and t1.business_date = @sSDate) dt,
   (select datediff(d, @sFirstZRead, @sSDate) as cpc) pc 





END
