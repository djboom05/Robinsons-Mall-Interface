USE [POS]
GO

/****** Object:  StoredProcedure [dbo].[sp_ZReading]    Script Date: 09/29/2021 11:10:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************** 
  Name: sp_ZReading
  Author: ariesteanila
  Report Group: 
  Report Title: Z-Reading
  Report Template: N/A
  Call by: ReportWriter.exe

  sp_ZReading '2018-09-01','2018-09-10',0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','','','','','',''
  
  LEGEND FOR DISCOUNT NAME3 FOR BIR REPORT:
	1 = SENIOR DISCOUNT
	2 = OTHER DISCOUNT GROSS
	3 = OTHER DISCOUNT WITH NET OF VAT
	4 = PWD DISCOUNT
	5 = VAT EXEMPT DISCOUNT / SALES TYPE
	6 = VAT EXEMPT w/CERTIFICATE
	7 = ZERO RATED DISCOUNT / SALES TYPE
	8 = UNUSED RESERVED
	9 = VAT ON REGULAR DISCOUNT
	10 = SERVICE CHARGE OTHER THAN TIP
	11 = TIP SERVICE CHARGE
	12 = PWD DISCOUNT included in VATEX SALES

  TENDER NAME3:
	2 = Other CARD
	3 = Other Tender

--Updates FEB072019
	+ isnull(sttl.Servchg,0) Removed this line in NetSales 
	+ isnull(sttl.Servchg,0) Removed this line in NetSalesWoVAT
	+ abs(isnull(sttl.toverring,0)) --removed this line in GrossSales
--End

***********************************************************/ 
ALTER PROCEDURE [dbo].[sp_ZReading]
@sSDate char(10),
@sEDate char(10),
@lSNumber Int, -- Object Number
@lENumber Int, -- Object Number
@lSRvc Int, -- Object Number
@lERvc Int, -- Object Number
@lSTIme Int, -- Object Number
@lETime Int, -- Object Number
@lSPC Int, -- Object Number
@lEPC Int, -- Object Number
@lSSalesType Int, -- Object Number
@lESalesType Int, -- Object Number
@lSTransType Int, -- Object Number
@lETransType Int, -- Object Number
@lSEmpl Int, -- Object Number
@lEEmpl Int, -- Object Number
@sNumber VarChar(100), -- All This variable is not use in Store Procedure
@sRvc VarChar(100),    -- It is for Crystal report use only
@sTime VarChar(100),
@sPC VarChar(100),
@sSalesType VarChar(100),
@sTransType VarChar(100),
@sEmpl VarChar(100)

AS

If(OBJECT_ID('tempdb..##temp_textFile') Is Not Null)
Begin
    Drop Table ##temp_textFile
End

Declare @sSql NVarChar(4000)
Declare @sSql2 NVarChar(4000)
Declare @sSql3 NVarChar(4000)

Set NoCount On

Set @sSql = 'select sttl.busidate [Date],
	round(isnull(sttl.Netsales,0),2) 
	+ isnull(sttl.Servchg,0)
	+ abs(isnull(scd.SCDTotal,0))
	+ abs(isnull(pwdNEW.PWDTotal,0))
	+ abs(isnull(VatExWC.VatExWC,0)+isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0))
	+ round(abs(isnull(pwd.PWDTotal,0)*1.12),2)
	+ round(abs(isnull(reg.RegDiscTotal,0)*1.12),2)
	+ round(abs(isnull(regV.RegDiscTotal,0)*1.12),2) GrossSales,
  case when abs(round(isnull(sttl.Netsales,0)-isnull(sttl.vattax1,0)+abs(isnull(VatExWC.VatExWC,0)+isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)),2)
	- round((abs(isnull(VatExWC.VatExWC,0))/.12),2) - round((abs(isnull(scd.SCDTotal,0))/.2*.8),2) - round((abs(isnull(pwdNEW.PWDTotal,0))/.2*.8),2) - round((abs(isnull(zerorated.ZeroRated,0))/.12),2))  < 0.1 then 0 else
	round(isnull(sttl.Netsales,0)-isnull(sttl.vattax1,0)+abs(isnull(VatExWC.VatExWC,0)+isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)),2)
	- round((abs(isnull(VatExWC.VatExWC,0))/.12),2) - round((abs(isnull(scd.SCDTotal,0))/.2*.8),2) - round((abs(isnull(pwdNEW.PWDTotal,0))/.2*.8),2) - round((abs(isnull(zerorated.ZeroRated,0))/.12),2) end  VATableSales,	
  (round((abs(isnull(zerorated.ZeroRated,0))/.12),2) + round((abs(isnull(VatExWC.VatExWC,0))/.12),2) + round((abs(isnull(scd.SCDTotal,0))/.2*0.8),2) + round((abs(isnull(pwdNEW.PWDTotal,0))/.2*0.8),2)) VATExemptSales,

  round(isnull(sttl.vattax1,0)+isnull(VatExWC.VatExWC,0)+isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)+isnull(vatreg.VatExempt,0),2) VATAmt,
  isnull(sttl.Servchg,0) SERVCHG, abs(isnull(scd.SCDTotal,0)) SCD, abs(isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)+isnull(VatExWC.VatExWC,0)) VATExempt, 
  (abs(isnull(pwdNEW.PWDTotal,0)) + round(abs(isnull(pwd.PWDTotal,0)*1.12),2)) PWD, round(abs(isnull(reg.RegDiscTotal,0)*1.12),2) RegDisc, 
  round(isnull(sttl.Netsales,0),2) NetSales,
  round(isnull(sttl.Netsales,0),2) 
	- round(abs(isnull(sttl.vattax1,0)+isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)+isnull(vatreg.VatExempt,0)),2) NetSalesWoVAT,
  round(case when abs(round(isnull(sttl.Netsales,0)+abs(isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)),2)) < 0.1 then 
	0 else round(isnull(sttl.Netsales,0)+abs(isnull(vatexempt.VatExempt,0)+isnull(zerorated.ZeroRated,0)),2) end * 0.07,2) CPF
'

Set @sSql2 = '
  ,abs(isnull(zerorated.ZeroRated,0)) ZeroRated,abs(isnull(zerorated.CZeroRated,0)) CZeroRated
  ,sttl.tvoid,sttl.toverring,sttl.pc,sttl.empl,sttl.cvoid,chkttl.coverring,tc.MinNo,tc.MaxNo
	INTO ##temp_textFile
  from (select busidate,pc,empl,sum(netsales)NetSales,sum(tservchg+tautosvc)Servchg,sum(tautosvc)Autosvc,
      sum(tax1 + tax2 + tax3 + tax4 + tax5 + tax6 + tax7 + tax8 + vattax1 + vattax2 + vattax3 + vattax4 + vattax5 + vattax6 + vattax7 + vattax8)Taxttl,
      sum(tax1  + tax2 + tax3 + tax4 + tax5 + tax6 + tax7 + tax8 + vattax1 + vattax2 + vattax3 + vattax4 + vattax5 + vattax6 + vattax7 + vattax8)vattax1,sum(toverring)toverring,sum(tvoid)tvoid,sum(cvoid)cvoid
    from salesttl st where st.busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' group by busidate,pc,empl)sttl 
	left join (select busidate, pc, empl, sum(coverring) coverring
	  from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' 
	  group by busidate, pc, empl)chkttl on sttl.busidate=chkttl.busidate and sttl.pc=chkttl.pc and sttl.empl=chkttl.empl
	left join (select busidate, pc, empl, sum(total) as VatExempt
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 = ''5'')
      group by busidate, pc, empl)vatexempt on sttl.busidate=vatexempt.busidate and sttl.pc=vatexempt.pc and sttl.empl=vatexempt.empl
    left join (select busidate, pc, empl, sum(total) as VatExempt
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 = ''9'')
      group by busidate, pc, empl)vatreg on sttl.busidate=vatreg.busidate and sttl.pc=vatreg.pc and sttl.empl=vatreg.empl
    left join (select busidate, pc, empl, sum(total) as ZeroRated, sum(count) as CZeroRated
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 = ''7'')
      group by busidate, pc, empl)zerorated on sttl.busidate=zerorated.busidate and sttl.pc=zerorated.pc and sttl.empl=zerorated.empl
'

set @sSql3 = '
    left join (select busidate, pc, empl, sum(total) as SCDTotal
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 = ''1'')
      group by busidate, pc, empl)scd on sttl.busidate = scd.busidate and sttl.pc=scd.pc and sttl.empl=scd.empl
    left join (select busidate, pc, empl, sum(total) as PWDTotal
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 = ''4'')
      group by busidate, pc, empl)pwd on sttl.busidate=pwd.busidate and sttl.pc=pwd.pc and sttl.empl=pwd.empl
    left join (select busidate, pc, empl, sum(total) as PWDTotal
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 = ''12'')
      group by busidate, pc, empl)pwdNEW on sttl.busidate=pwdNEW.busidate and sttl.pc=pwdNEW.pc and sttl.empl=pwdNEW.empl
	left join (select busidate, pc, empl, sum(total) as RegDisctotal
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id IN (select id from discount where name3 in (''2''))
      group by busidate, pc, empl)reg on sttl.busidate=reg.busidate and sttl.pc=reg.pc and sttl.empl=reg.empl 
	left join (select busidate, pc, empl, sum(total) as RegDisctotal
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id IN (select id from discount where name3 in (''3''))
      group by busidate, pc, empl)regV on sttl.busidate=regV.busidate and sttl.pc=regV.pc and sttl.empl=regV.empl 
    left join (select busidate, pc, empl,sum(total) as VatExWC
      from discttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id IN (select id from discount where name3 = ''6'')
      group by busidate, pc, empl)VatExWC on sttl.busidate=VatExWC.busidate and sttl.pc=VatExWC.pc and sttl.empl=VatExWC.empl 
    left join (select busidate, pc, empl, sum(total) as service
      from Servchgttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from servchg where name3 = ''10'')
      group by busidate, pc, empl)AutoSvc on sttl.busidate=AutoSvc.busidate and sttl.pc=AutoSvc.pc and sttl.empl=AutoSvc.empl
	left join (select busidate, pc, empl, sum(total) as service
      from Servchgttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from servchg where name3 = ''11'')
      group by busidate, pc, empl)Servchg on sttl.busidate=Servchg.busidate and sttl.pc=Servchg.pc and sttl.empl=Servchg.empl 
    left join (select busidate,id, min(thaitax)MinNo,max(thaitax)MaxNo
      from (select business_date as busidate, pc.id, dbo.checks_tax(checks.remark)thaitax
          from checks,transactions,pc
          where checks.chk_seq=transactions.chk_seq and transactions.paid=1 
          and business_date between ''' + @sSDate + ''' and ''' + @sEDate + ''' and transactions.pc_seq=pc.id
          and transactions.training=0 and charindex(substring(transactions.status,2,1),''4567CDEF'')=0
          and type <> ''C'' and dbo.checks_tax(checks.remark) <> ''''
          and (xfer_chk_num is null or charindex(substring(checks.status,6,1),''2367ABEF'')>0)) tax
      group by busidate, id) tc on sttl.busidate = tc.busidate and sttl.pc = tc.id '

Exec (@sSql + @sSql2 + @sSql3)


DECLARE @col1 datetime, @col2 varchar(50),
	@col3 varchar(50), @col4 decimal(16,4),
	@col5 decimal(16,4), @col6 decimal(16,4),  
	@col7 decimal(16,4), @col8 decimal(16,4),  
	@col9 decimal(16,4), @col10 decimal(16,4),  
	@col11 decimal(16,4), @col12 decimal(16,4),  
	@col13 decimal(16,4), @col14 decimal(16,4),
	@col15 decimal(16,4), @col16 decimal(16,4),
	@col17 decimal(16,4), @col18 decimal(16,4),
	@col19 decimal(16,4), @col20 decimal(16,4),
	@col21 decimal(16,4), @col22 decimal(16,4),
	@col23 decimal(16,4), @col24 decimal(16,4),
	@col25 decimal(16,4), @col26 decimal(16,4),
	@col27 decimal(16,4), @col28 decimal(16,4),
	@col29 decimal(16,4), @col30 decimal(16,4),
	@col31 decimal(16,4), @col32 decimal(16,4),
	@col33 decimal(16,4), @col34 decimal(16,4),
	@col35 decimal(16,4), @col36 decimal(16,4),
	@col37 decimal(16,4), @col38 decimal(16,4),
	@col39 decimal(16,4), @col40 decimal(16,4),
	@col41 decimal(16,4), @col42 decimal(16,4),
	@col43 decimal(16,4), @col44 decimal(16,4),
	@col45 decimal(16,4), @col46 decimal(16,4),
	@col47 decimal(16,4), @col48 decimal(16,4),
	@col49 decimal(16,4), @col50 decimal(16,4),
	@col51 decimal(16,4), @col52 decimal(16,4),
	@col53 decimal(16,4), @col54 decimal(16,4),
	@col55 decimal(16,4), @col56 decimal(16,4),
	@col57 decimal(16,4), @col58 decimal(16,4),
	@col59 decimal(16,4), @col60 int,
	@col61 int, @col62 int,
	@col63 decimal(16,4), @col64 decimal(16,4),
	@col65 decimal(16,4), @col66 decimal(16,4),
	@col67 decimal(16,4), @col68 decimal(16,4)
		
Declare @sCondition VarChar(1000)
Declare @sColumn VarChar(1000)
Declare @sGroup VarChar(1000)
Declare @ReportSubTitle as NVarChar(4000)

set @ReportSubTitle = (Select rtrim(ltrim(guesttrailer1))+'^'+rtrim(ltrim(guesttrailer2))+'^'+rtrim(ltrim(guesttrailer3))+'^'+rtrim(ltrim(guesttrailer4))+'^'+rtrim(ltrim(guesttrailer5))+', '+ltrim(rtrim(guesttrailer6))+' '+ltrim(rtrim(guesttrailer7)) FROM trailer Where number = 52)

Set @sCondition = ''
Set @sColumn = ' , ''-'' PC1, ''-'' as EMPL '
Set @sGroup = ' '

If @lSPC <> 0 or @lEPC <> 0 
BEGIN
  Set @sCondition = @sCondition + ' and PC in (select id from pc where number Between ' + 
                      Convert(VarChar, @lSPC) + ' and ' + Convert(VarChar, @lEPC) + ') '
END

If @lSEmpl <> 0 or @lEEmpl <> 0 
BEGIN
  Set @sCondition = @sCondition + ' and EMPL in (select id from empldef where number Between ' + 
                      Convert(VarChar, @lSEmpl) + ' and ' + Convert(VarChar, @lEEmpl) + ') '
END

DECLARE @TxtTable table (descript varchar(50), value varchar(100),transdate datetime, 
						 PC varchar(100), EMP varchar(100), HEADER nvarchar(4000),
						 FOOTER varchar(100), ROWTYPE nvarchar(10), ISFONTBOLD int)

DECLARE @sqlstatement nvarchar(4000)
DECLARE @sqlstatement2 nvarchar(4000)
DECLARE @sqlstatement3 nvarchar(4000)

SET @sqlstatement = 'DECLARE txt_cursor CURSOR FOR
Select getdate() transdate' + @sColumn + ' , SUM(tblA.GrossSales)GrossSales,SUM(tblA.VATableSales)VATableSales,SUM(tblA.VATExemptSales)VATExemptSales 
,SUM(tblA.VATAmt)VATAmt,SUM(tblA.SERVCHG)SERVCHG,SUM(tblA.SCD)SCD,SUM(tblA.VATExempt)VATExempt
,SUM(tblA.PWD)PWD,SUM(tblA.RegDisc)RegDisc,SUM(tblA.NetSales)NetSales,SUM(tblA.NetSalesWoVAT)NetSalesWoVAT 
,abs(SUM(tblA.tvoid))VOID,abs(SUM(tblA.toverring))REFUND
,ISNULL((SELECT SUM(count) ctr from MajorTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from major where number = 1) ' + @sGroup + '),0) CFOOD
,ISNULL((SELECT SUM(count) ctr from MajorTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from major where number = 2) ' + @sGroup + '),0) CBEV
,ISNULL((SELECT SUM(count) ctr from MajorTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from major where number = 3) ' + @sGroup + '),0) COTHER
,ABS(ISNULL((SELECT SUM(total) total from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)*1.12) EmpDisc
,ABS(ISNULL((SELECT SUM(total) total from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)*1.12) Disc5Per
,ABS(ISNULL((SELECT SUM(total) total from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)*1.12) Disc10Per
,ABS(ISNULL((SELECT SUM(total)*1.12 total from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 in (''2'')) ' + @sGroup + '),0)) DiscOTH
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (1)) ' + @sGroup + '),0) CASH
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (101)) ' + @sGroup + '),0) MASTER
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (102)) ' + @sGroup + '),0) VISA
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (103)) ' + @sGroup + '),0) AMEX
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (105)) ' + @sGroup + '),0) JCB
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where name3 in (''3'')) ' + @sGroup + '),0) OTHERTENDER
,ISNULL((SELECT SUM(netsales) total from SalesTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type in (1) ' + @sGroup + '),0) TDineIn
,ISNULL((SELECT SUM(netsales) total from SalesTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type in (25) ' + @sGroup + '),0) TTakeOut
,ISNULL((SELECT SUM(netsales) total from SalesTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type not in (1,25) ' + @sGroup + '),0) TOthTrans
,abs(SUM(tblA.cvoid))CVOID,abs(SUM(tblA.coverring))CREFUND '

SET @sqlstatement2 = ' ,ISNULL((SELECT SUM(covers) covers from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type in (1) ' + @sGroup + '),0) GCDineIn
,ISNULL((SELECT SUM(covers) covers from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type in (25) ' + @sGroup + '),0) GCTakeOut
,ISNULL((SELECT SUM(covers) covers from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type not in (1,25) ' + @sGroup + '),0) GCOthTrans
,ISNULL((SELECT SUM(paid) paid from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type in (1) ' + @sGroup + '),0) CDineIn
,ISNULL((SELECT SUM(paid) paid from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type in (25) ' + @sGroup + '),0) CTakeOut
,ISNULL((SELECT SUM(paid) paid from checkttl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      trans_type not in (1,25) ' + @sGroup + '),0) COthTrans
,ISNULL((SELECT SUM(total) total from MajorTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from major where number = 1) ' + @sGroup + '),0) TFOOD
,ISNULL((SELECT SUM(total) total from MajorTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from major where number = 2) ' + @sGroup + '),0) TBEV
,ISNULL((SELECT SUM(total) total from MajorTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from major where number = 3) ' + @sGroup + '),0) TOTHER
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (101)) ' + @sGroup + '),0)) CSCDisc
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (201,202)) ' + @sGroup + '),0)) CVATDisc
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (102)) ' + @sGroup + '),0)) CPWDDisc
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)) CEMPDisc
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)) CDisc5
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)) CDisc10
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)) CDisc20
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where name3 in (''3'')) ' + @sGroup + '),0)) CDiscOTH
,ABS(ISNULL((SELECT SUM(total) total from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (0)) ' + @sGroup + '),0)*1.12) Disc20Per '

SET @sqlstatement3 = ' ,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (1)) ' + @sGroup + '),0) CCASH
,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (101)) ' + @sGroup + '),0) CMASTER
,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (102)) ' + @sGroup + '),0) CVISA
,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (103)) ' + @sGroup + '),0) CAMEX
,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where number in (105)) ' + @sGroup + '),0) CJCB
,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where name3 in (''3'')) ' + @sGroup + '),0) COTHERTENDER
,ISNULL((SELECT SUM(grandtotal) total from GrandTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' 
       ' + @sGroup + '),0) NewGrandTotal, MIN(tblA.MinNo), MAX(tblA.MaxNo)  
,ISNULL((SELECT COUNT(*) ctr from GrandTtl where busidate <=''' + @sEDate + ''' 
       and rvc_seq=1 ' + @sGroup + '),0) EODCOUNT
,ABS(ISNULL((SELECT SUM(total) total from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (230)) ' + @sGroup + '),0)) Diplomat
,ABS(ISNULL((SELECT SUM(count) ctr from DiscTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from discount where number in (230)) ' + @sGroup + '),0)) CDiplomat
,SUM(tblA.ZeroRated)ZeroRated,SUM(tblA.CZeroRated)CZeroRated
,ISNULL((SELECT SUM(total) total from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where name3 in (''2'')) ' + @sGroup + '),0) OTHERCARD
,ISNULL((SELECT SUM(count) count from TenderTtl where busidate between ''' + @sSDate + ''' and ''' + @sEDate + ''' and
      id in (select id from tender where name3 in (''2'')) ' + @sGroup + '),0) COTHERCARD
from ##temp_textFile tblA where 1=1 ' + @sCondition + @sGroup + ' '

EXEC(@sqlstatement + @sqlstatement2 + @sqlstatement3)

OPEN txt_cursor
FETCH NEXT FROM txt_cursor   
INTO @col1, @col2, @col3, @col4, @col5, @col6, @col7, @col8, @col9 ,
@col10, @col11, @col12, @col13, @col14, @col15, @col16, @col17, @col18
,@col19,@col20,@col21,@col22,@col23,@col24,@col25,@col26,@col27,@col28,@col29
,@col30,@col31,@col32,@col33,@col34,@col35,@col36,@col37,@col38,@col39,
@col40,@col41,@col42,@col43,@col44,@col45,@col46,@col47,@col48,@col49,@col50,
@col51,@col52,@col53,@col54,@col55,@col56,@col57,@col58,@col59,@col60,@col61
,@col62,@col63,@col64,@col65,@col66,@col67,@col68

WHILE @@FETCH_STATUS = 0
BEGIN

	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
	INSERT INTO @TxtTable VALUES ('EOD Count',	@col62, @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)	
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('New Grand Total',	CAST(CONVERT(DECIMAL(16,2),@col59) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)	
	INSERT INTO @TxtTable VALUES ('Old Grand Total',	CAST(CONVERT(DECIMAL(16,2),@col59-(@col24+@col25+@col26+@col27+@col28+@col29+@col67)) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)	
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('Gross Sales',	CAST(CONVERT(DECIMAL(16,2),@col4) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('PWD',			CAST(CONVERT(DECIMAL(16,2),@col11) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('SCD Disc',		CAST(CONVERT(DECIMAL(16,2),@col9) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('SCD/PWD VAT Disc',	CAST(CONVERT(DECIMAL(16,2),@col10-@col63-@col65) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Diplomat',		CAST(CONVERT(DECIMAL(16,2),@col63) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Zero-Rated',		CAST(CONVERT(DECIMAL(16,2),@col65) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Other Disc',		CAST(CONVERT(DECIMAL(16,2),@col26+@col23) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Service Charge',	CAST(CONVERT(DECIMAL(16,2),@col8) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Net Sales w/VAT',		CAST(CONVERT(DECIMAL(16,2),@col13) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('VAT Exempt Sales',CAST(CONVERT(DECIMAL(16,2),@col6) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('VAT Amount',		CAST(CONVERT(DECIMAL(16,2),@col7) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Total VAT Sales',CAST(CONVERT(DECIMAL(16,2),@col5) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Net Sales w/oVAT',CAST(CONVERT(DECIMAL(16,2),@col14) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('Total Void		' + cast(convert(int,@col33) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col15) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Total Refund		' + cast(convert(int,@col34) as nvarchar),	CAST(CONVERT(DECIMAL(16,2),@col16) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)

	INSERT INTO @TxtTable VALUES ('*Trans Sales*',	'', @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('Dine In			' + cast(convert(int,@col38) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col30) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Take Home			' + cast(convert(int,@col39) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col31) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Other				' + cast(convert(int,@col40) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col32) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Total				' + cast(convert(int,@col38+@col39+@col40) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col30+@col31+@col32) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)

	INSERT INTO @TxtTable VALUES ('*Guest Count/AVE Cover*',	'', @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('Dine In			' + cast(convert(int,@col35) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),isnull(@col30/nullif(@col35,0),0)) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Take Home			' + cast(convert(int,@col36) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),isnull(@col31/nullif(@col36,0),0)) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Other				' + cast(convert(int,@col37) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),isnull(@col32/nullif(@col37,0),0)) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Total				' + cast(convert(int,@col35+@col36+@col37) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),(@col30+@col31+@col32)/nullif((@col35+@col36+@col37),0)) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)

	INSERT INTO @TxtTable VALUES ('*Major Group*',	'', @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('FOOD				' + cast(convert(int,@col17) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col41) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('BEVERAGE			' + cast(convert(int,@col18) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col42) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('OTHERS				' + cast(convert(int,@col19) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col43) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Total				' + cast(convert(int,@col17+@col18+@col19) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col41+@col42+@col43) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)

	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*Discounts*',	'', @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('SCD Disc			' + cast(convert(int,@col44) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col9) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('SCD/PWD VAT Disc	' + cast(convert(int,@col45) as nvarchar),	CAST(CONVERT(DECIMAL(16,2),@col10-@col63-@col65) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('PWD					' + cast(convert(int,@col46) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col11) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('StockHolder		' + cast(convert(int,@col47) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col20) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Loyalty 5% Disc	' + cast(convert(int,@col48) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col21) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Loyalty 10% Disc	' + cast(convert(int,@col49) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col22) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('20% Disc			' + cast(convert(int,@col50) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col52) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Other Discounts	' + cast(convert(int,@col51) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col23) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Diplomat			' + cast(convert(int,@col64) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col63) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Zero-Rated		' + cast(convert(int,@col66) as nvarchar),		CAST(CONVERT(DECIMAL(16,2),@col65) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Total Discounts	' + cast(convert(int,@col44+@col45+@col46+@col47+@col48+@col49+@col50+@col51+@col64) as nvarchar),CAST(CONVERT(DECIMAL(16,2),@col9+@col10+@col11+@col20+@col21+@col22+@col23+@col52) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*Payment*',		'', @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
	INSERT INTO @TxtTable VALUES ('CASH				' + cast(convert(int,@col53) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col24) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('MASTER				' + cast(convert(int,@col54) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col25) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('VISA				' + cast(convert(int,@col55) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col26) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('AMEX				' + cast(convert(int,@col56) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col27) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('JCB					' + cast(convert(int,@col57) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col28) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('Other Card			' + cast(convert(int,@col68) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col67) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('OTHERS				' + cast(convert(int,@col58) as nvarchar),			CAST(CONVERT(DECIMAL(16,2),@col29) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
	INSERT INTO @TxtTable VALUES ('Total Payments	' + cast(convert(int,@col53+@col54+@col55+@col56+@col57+@col58+@col68) as nvarchar),	CAST(CONVERT(DECIMAL(16,2),@col24+@col25+@col26+@col27+@col28+@col29+@col67) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
--	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
--	INSERT INTO @TxtTable VALUES ('*Shift Rpt*',	'', @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)
--	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
--	INSERT INTO @TxtTable VALUES ('Actual Cash',	CAST(CONVERT(DECIMAL(16,2),@col24) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
--	INSERT INTO @TxtTable VALUES ('Cash Expected',	CAST(CONVERT(DECIMAL(16,2),@col24) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)
--	INSERT INTO @TxtTable VALUES ('-',				'', @col1, @col2, @col3, @ReportSubTitle, '', '-', 0)
--	INSERT INTO @TxtTable VALUES ('Cash Over/Short',CAST(CONVERT(DECIMAL(16,2),@col24-@col24) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 1)

	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('Beginning OR Number',	CAST(CONVERT(int,@col60) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)	
	INSERT INTO @TxtTable VALUES ('Ending OR Number',	CAST(CONVERT(int,@col61) as nvarchar), @col1, @col2, @col3, @ReportSubTitle, '', 'NORMAL', 0)	
	INSERT INTO @TxtTable VALUES ('<BR>',			'', @col1, @col2, @col3, @ReportSubTitle, '', '<BR>', 0)
	INSERT INTO @TxtTable VALUES ('*',				'', @col1, @col2, @col3, @ReportSubTitle, '', '*', 0)
		
	FETCH NEXT FROM txt_cursor   
    INTO @col1, @col2, @col3, @col4, @col5, @col6, @col7, @col8, @col9 ,
		@col10, @col11, @col12, @col13, @col14,@col15, @col16, @col17, @col18, 
		@col19,@col20,@col21,@col22,@col23,@col24,@col25,@col26,@col27,@col28,
		@col29,@col30,@col31,@col32,@col33,@col34,@col35,@col36,@col37,@col38,
		@col39,@col40,@col41,@col42,@col43,@col44,@col45,@col46,@col47,@col48,
		@col49,@col50,@col51,@col52,@col53,@col54,@col55,@col56,@col57,@col58,
		@col59,@col60,@col61,@col62,@col63,@col64,@col65,@col66,@col67,@col68

END   
CLOSE txt_cursor
DEALLOCATE txt_cursor

If(OBJECT_ID('tempdb..##temp_textFile') Is Not Null)
Begin
    Drop Table ##temp_textFile
End
Select * FROM @TxtTable

GO

