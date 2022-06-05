@echo off

	echo Action #3 - Running SQL scripts on %COMPUTERNAME%
	echo %DATE% %TIME% BEGIN >> Install-SQL.log
	osql -S(local) -dPOS -Udatascan -PDTSbsd7188228 -imallinterface_batchlogs table.sql -e >> Install-SQL.log
	osql -S(local) -dPOS -Udatascan -PDTSbsd7188228 -isp_SalesDailyRobMallTxtfile.sql -e >> Install-SQL.log
	echo %DATE% %TIME% END >> Install-SQL.log

echo DONE
pause