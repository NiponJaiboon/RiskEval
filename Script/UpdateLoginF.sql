select * from [dbo].[UserSession]
where SignoutTS='2300-12-31 00:00:00.000'

--select * from  users

update  users 
set LastLoginTimestamp=getdate(),LastFailedLoginTimestamp=getdate()
where UserID='1018'

update  UserSession 
set SignoutTS = DATEADD(minute, 1 ,SigninTS) 

where SignoutTS='2300-12-31 00:00:00.000'



select * 
from [UserSession] us
inner join Users u on u.UserID=us.UserID 
and u.UserID='1018'

--LastFailedLoginTimestamp
--1800-01-01 00:00:00.000
