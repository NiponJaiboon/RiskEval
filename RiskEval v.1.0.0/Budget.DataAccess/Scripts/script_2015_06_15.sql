CREATE TABLE Strategic (
	ID int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Name nvarchar(800) NOT NULL,
	EffectiveFrom datetime null,
	EffectiveTo datetime null,
	CreatedBy int null,
	CreatedTS datetime null
);

INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'1 ยุทธศาสตร์เร่งรัดวางรากฐานการพัฒนาที่ยั่งยืนของประเทศ', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'2 ยุทธศาสตร์ความมั่นคงแห่งรัฐ', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'3 ยุทธศาสตร์การสร้างความเจริญเติบโตทางเศรษฐกิจอย่างมีเสถียรภาพและยั่งยืน', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'4 ยุทธศาสตร์การศึกษา คุณธรรม จริยธรรม คุณภาพชีวิต และความเท่าเทียมกันในสังคม', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'5 ยุทธศาสตร์การอนุรักษ์ ฟื้นฟูทรัพยากรธรรมชาติและสิ่งแวดล้อม', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'6 ยุทธศาสตร์การพัฒนาวิทยาศาสตร์ เทคโนโลยี การวิจัยและนวัตกรรม', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'7 ยุทธศาสตร์การต่างประเทศและเศรษฐกิจระหว่างประเทศ', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT Strategic (Name, EffectiveFrom, EffectiveTo) VALUES (N'8 ยุทธศาสตร์การบริหารกิจการบ้านเมืองที่ดี', '2015-01-01 00:00:00','2300-12-31 00:00:00')


CREATE TABLE GoodGovernance (
	ID int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Name nvarchar(800) NOT NULL,
	EffectiveFrom datetime null,
	EffectiveTo datetime null,
	CreatedBy int null,
	CreatedTS datetime null
);

INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักการมีส่วนร่วมของสาธารณะ', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักความโปร่งใส', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักความรับผิดรับชอบต่อสาธารณะ', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักความเสมอภาค', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักนิติธรรม', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักคุณธรรม', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักการสนองตอบรับ', '2015-01-01 00:00:00','2300-12-31 00:00:00')
INSERT GoodGovernance (Name, EffectiveFrom, EffectiveTo) VALUES (N'หลักความคุ้มค่า', '2015-01-01 00:00:00','2300-12-31 00:00:00')
