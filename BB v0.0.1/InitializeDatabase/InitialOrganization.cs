using Budget;
using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitializeDatabase
{
    public class InitialOrganization
    {
        private SessionContext context;
        public InitialOrganization(SessionContext context)
        {
            this.context = context;

            Create01000();//สำนักนายกรัฐมนตรี
            Create02000();//กระทรวงกลาโหม
            Create03000();//กระทรวงการคลัง
            Create04000();//กระทรวงการต่างประเทศ
            Create05000();//กระทรวงการท่องเที่ยวและกีฬา
            Create06000();//กระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์
            Create07000();//กระทรวงเกษตรและสหกรณ์
            Create08000();//กระทรวงคมนาคม
            Create09000();//กระทรวงทรัพยากรธรรมชาติและสิ่งแวดล้อม
            Create11000();//กระทรวงเทคโนโลยีสารสนเทศและการสื่อสาร
            Create12000();//กระทรวงพลังงาน
            Create13000();//กระทรวงพาณิชย์
            Create14000();//ทบวงมหาวิทยาลัย
            Create15000();//กระทรวงมหาดไทย
            Create16000();//กระทรวงยุติธรรม
            Create17000();//กระทรวงแรงงาน
            Create18000();//กระทรวงวัฒนธรรม
            Create19000();//กระทรวงวิทยาศาสตร์และเทคโนโลยี
            Create20000();//กระทรวงศึกษาธิการ
            Create21000();//กระทรวงสาธารณสุข
            Create22000();//กระทรวงอุตสาหกรรม
            Create25000();//หน่วยงานขององค์กรตามรัฐธรรมนูญ และหน่วยงานอิสระตามรัฐธรรมนูญ
            Create26000();//หน่วยงานอิสระตามรัฐธรรมนูญ
            Create70000();//จังหวัดและกลุ่มจังหวัด
            Create50000();//รัฐวิสาหกิจ
            Create60000();//สภากาชาดไทย
            Create80800();//กองทุนและเงินทุนหมุนเวียน
        }

        private void Create01000()
        {
            PersistGovernmentUnit
            (
                "01000", CreateOrgName("สำนักนายกรัฐมนตรี", "Office of the Prime Minister"),
                new List<OrgUnit>
                {          
                    new OrgUnit{ Code = "01001", CurrentName = CreateOrgName("สำนักงานปลัดสำนักนายกรัฐมนตรี","Office of the Secretariat") },
                    new OrgUnit{ Code = "01002", CurrentName = CreateOrgName("กรมประชาสัมพันธ์","The Government Public Relations Department") },
                    new OrgUnit{ Code = "01003", CurrentName = CreateOrgName("สำนักงานคณะกรรมการคุ้มครองผู้บริโภค","Office of the Consumer Protection Board") },
                    new OrgUnit{ Code = "01004", CurrentName = CreateOrgName("สำนักเลขาธิการนายกรัฐมนตรี","The Secretariat of the Prime Minister") },
                    new OrgUnit{ Code = "01005", CurrentName = CreateOrgName("สำนักเลขาธิการคณะรัฐมนตรี","The Secretariat of the Cabinet") },
                    new OrgUnit{ Code = "01006", CurrentName = CreateOrgName("สำนักข่าวกรองแห่งชาติ","National Intelligence Agency") },
                    new OrgUnit{ Code = "01007", CurrentName = CreateOrgName("สำนักงบประมาณ","Bureau of the Budget") },
                    new OrgUnit{ Code = "01008", CurrentName = CreateOrgName("สำนักงานสภาความมั่นคงแห่งชาติ","Secretariat of the National Security Council") },
                    new OrgUnit{ Code = "01009", CurrentName = CreateOrgName("สำนักงานคณะกรรมการกฤษฎีกา","Office of the Juridical Council") },
                    new OrgUnit{ Code = "01011", CurrentName = CreateOrgName("สำนักงานคณะกรรมการข้าราชการพลเรือน","Office of the Civil Service Commission") },
                    new OrgUnit{ Code = "01012", CurrentName = CreateOrgName("สำนักงานคณะกรรมการพัฒนาการเศรษฐกิจและสังคมแห่งชาติ","Office of the National Economic and Social Development Board") },
                    new OrgUnit{ Code = "01014", CurrentName = CreateOrgName("สำนักงานกองทุนสนับสนุนการวิจัย","Research Fund") },
                    new OrgUnit{ Code = "01016", CurrentName = CreateOrgName("สำนักงานรับรองมาตรฐานและประเมินคุณภาพการศึกษา(องค์การมหาชน)","Office for National Education Standards and Quality Assessment (Public Organization) ") },
                    new OrgUnit{ Code = "01019", CurrentName = CreateOrgName("กองอำนวยการรักษาความมั่นคงภายในราชอาณาจักร","Director of the Internal Security Division of the Kingdom.") },
                    new OrgUnit{ Code = "01021", CurrentName = CreateOrgName("สำนักงานคณะกรรมการพัฒนาระบบราชการ","Office of Public Sector Development") },
                    new OrgUnit{ Code = "01023", CurrentName = CreateOrgName("องค์การบริหารการพัฒนาพื้นที่พิเศษเพื่อการท่องเที่ยวอย่างยั่งยืน(องค์การมหาชน)","Administration of Designated Areas for Sustainable Tourism (ITD).") },
                    new OrgUnit{ Code = "01024", CurrentName = CreateOrgName("สำนักงานส่งเสริมการจัดประชุมและนิทรรศการ(องค์การมหาชน)","Thailand Convention and Exhibition Bureau (Public Organization).") },
                    new OrgUnit{ Code = "01025", CurrentName = CreateOrgName("สำนักงานบริหารและพัฒนาองค์ความรู้(องค์การมหาชน)","Office of Management and Cognitive Development (ITD.)") },
                    new OrgUnit{ Code = "01027", CurrentName = CreateOrgName("สำนักงานคณะกรรมการสุขภาพแห่งชาติ","Board of Health") },
                    new OrgUnit{ Code = "01028", CurrentName = CreateOrgName("สถาบันบริหารจัดการธนาคารที่ดิน(องค์การมหาชน)","Land Bank Management Institute (ITD)") },
                    new OrgUnit{ Code = "01029", CurrentName = CreateOrgName("สถาบันคุณวุฒิวิชาชีพ(องค์การมหาชน)","Institute of professional qualifications (ITD).") },
                    new OrgUnit{ Code = "01031", CurrentName = CreateOrgName("สำนักงานพัฒนาพิงคนคร(องค์การมหาชน)","Ping City Development Agency (ITD).") },
                    new OrgUnit{ Code = "01032", CurrentName = CreateOrgName("สำนักงานคณะกรรมการส่งเสริมการลงทุน","Office of the Board of Investment") },
                }
            );
        }

        private void Create02000()
        {
            PersistGovernmentUnit
            (
                "02000", CreateOrgName("กระทรวงกลาโหม", "Ministry of Defence"),
                new List<OrgUnit>
                {          
                    new OrgUnit{ Code = "02001", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงกลาโหม","Ministry of Defence")},
                    new OrgUnit{ Code = "02002", CurrentName = CreateOrgName("กรมราชองครักษ์","Office of A.D.C.")},
                    new OrgUnit{ Code = "02004", CurrentName = CreateOrgName("กองทัพบก","Army")},
                    new OrgUnit{ Code = "02005", CurrentName = CreateOrgName("กองทัพเรือ","Fleet")},
                    new OrgUnit{ Code = "02006", CurrentName = CreateOrgName("กองทัพอากาศ","Air force")},
                    new OrgUnit{ Code = "02008", CurrentName = CreateOrgName("กองบัญชาการกองทัพไทย","Garrison Thailand")},
                    new OrgUnit{ Code = "02009", CurrentName = CreateOrgName("สถาบันเทคโนโลยีป้องกันประเทศ(องค์การมหาชน)","Defence Technology Institute (Public Organization).")},
                }
            );
        }

        private void Create03000()
        {
            PersistGovernmentUnit
            (
                "03000", CreateOrgName("กระทรวงการคลัง", "Ministry of Finance"),
                new List<OrgUnit>
                {          
                    new OrgUnit{ Code = "03001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "03002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงการคลัง","Ministry of Finance")},
                    new OrgUnit{ Code = "03003", CurrentName = CreateOrgName("กรมธนารักษ์","Treasury Department")},
                    new OrgUnit{ Code = "03004", CurrentName = CreateOrgName("กรมบัญชีกลาง","Comptroller General's Department")},
                    new OrgUnit{ Code = "03005", CurrentName = CreateOrgName("กรมศุลกากร","Customs Department")},
                    new OrgUnit{ Code = "03006", CurrentName = CreateOrgName("กรมสรรพสามิต","Excise Department")},
                    new OrgUnit{ Code = "03007", CurrentName = CreateOrgName("กรมสรรพากร","Revenue Department")},
                    new OrgUnit{ Code = "03008", CurrentName = CreateOrgName("สำนักงานคณะกรรมการนโยบายรัฐวิสาหกิจ","State Enterprise Policy Office")},
                    new OrgUnit{ Code = "03009", CurrentName = CreateOrgName("สำนักงานบริหารหนี้สาธารณะ","Public Debt Management Office")},
                    new OrgUnit{ Code = "03011", CurrentName = CreateOrgName("สำนักงานเศรษฐกิจการคลัง","Fiscal Policy Office")},
                    new OrgUnit{ Code = "03012", CurrentName = CreateOrgName("สำนักงานความร่วมมือพัฒนาเศรษฐกิจกับประเทศเพื่อนบ้าน(องค์การมหาชน)","Neighbouring Countries Economic Development Cooperation Agency (Public Organization)")},
                }
            );
        }

        private void Create04000()
        {
            PersistGovernmentUnit
            (
                "04000", CreateOrgName("กระทรวงการต่างประเทศ", "Ministry of Foreign Affairs"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "04001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "04002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงการต่างประเทศ","Ministry of Foreign Affairs")},
                    new OrgUnit{ Code = "04003", CurrentName = CreateOrgName("กรมการกงสุล","Consular Affairs")},
                    new OrgUnit{ Code = "04004", CurrentName = CreateOrgName("กรมพิธีการทูต","Department of Protocol")},
                    new OrgUnit{ Code = "04005", CurrentName = CreateOrgName("กรมยุโรป","Department of European Affairs")},
                    new OrgUnit{ Code = "04006", CurrentName = CreateOrgName("กรมวิเทศสหการ","Department of Technical and Economic Cooperation")},
                    new OrgUnit{ Code = "04007", CurrentName = CreateOrgName("กรมเศรษฐกิจระหว่างประเทศ","Department of International Economics")},
                    new OrgUnit{ Code = "04008", CurrentName = CreateOrgName("กรมสนธิสัญญาและกฎหมาย","Treaty and Law Department")},
                    new OrgUnit{ Code = "04009", CurrentName = CreateOrgName("กรมสารนิเทศ","Department of Information")},
                    new OrgUnit{ Code = "04011", CurrentName = CreateOrgName("กรมองค์การระหว่างประเทศ","Department of International Organizations")},
                    new OrgUnit{ Code = "04012", CurrentName = CreateOrgName("กรมอเมริกาและแปซิฟิกใต้","Department of American and South Pacific Affairs")},
                    new OrgUnit{ Code = "04013", CurrentName = CreateOrgName("กรมอาเซียน","Department of ASEAN Affairs")},
                    new OrgUnit{ Code = "04014", CurrentName = CreateOrgName("กรมเอเชียตะวันออก","Department of East Asian")},
                    new OrgUnit{ Code = "04015", CurrentName = CreateOrgName("กรมเอเชียใต้ ตะวันออกกลางและแอฟริกา","Department of South Asian, Middle East and Africa")},
                }
            );
        }

        private void Create05000()
        {
            PersistGovernmentUnit
            (
                "05000", CreateOrgName("กระทรวงการท่องเที่ยวและกีฬา", "Ministry of Tourism and Sports"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "05001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "05002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงการท่องเที่ยวและกีฬา","Ministry of Tourism and Sports.")},
                    new OrgUnit{ Code = "05003", CurrentName = CreateOrgName("กรมพลศึกษา","Department of Physical Education")},
                    new OrgUnit{ Code = "05004", CurrentName = CreateOrgName("กรมการท่องเที่ยว","Department of Tourism")},
                    new OrgUnit{ Code = "05006", CurrentName = CreateOrgName("สถาบันการพลศึกษา","Institute of Physical Education")},
                }
            );
        }

        private void Create06000()
        {
            PersistGovernmentUnit
            (
                "06000", CreateOrgName("กระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์", "Ministry of Social Development and Human Security"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "06001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "06002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์","Ministry of Social Development and Human Security.")},
                    new OrgUnit{ Code = "06003", CurrentName = CreateOrgName("กรมพัฒนาสังคมและสวัสดิการ","Department of Social Development and Welfare")},
                    new OrgUnit{ Code = "06004", CurrentName = CreateOrgName("สำนักงานกิจการสตรีและสถาบันครอบครัว","Office of Women's Affairs and Family Development")},
                    new OrgUnit{ Code = "06005", CurrentName = CreateOrgName("สำนักงานส่งเสริมสวัสดิภาพและพิทักษ์เด็ก เยาวชน ผู้ด้อยโอกาส และผู้สูงอายุ","The Office of Welfare Promotion and Protection of children, youth, the disadvantaged and the elderly.")},
                    new OrgUnit{ Code = "06006", CurrentName = CreateOrgName("สถาบันพัฒนาองค์กรชุมชน(องค์การมหาชน)","Community Organizations Development Institute (Public Organization).")},
                    new OrgUnit{ Code = "06007", CurrentName = CreateOrgName("สำนักงานส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการแห่งชาติ","Office for Empowerment of Persons with Disabilities.")},
                }
            );
        }

        private void Create07000()
        {
            PersistGovernmentUnit
            (
                "07000", CreateOrgName("กระทรวงเกษตรและสหกรณ์", "Ministry of Agriculture and Cooperatives"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "07001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "07002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงกษตรและสหกรณ์","Office of the Permanent Secretary Ministry of Agriculture and Cooperatives and the Cooperative Sq.")},
                    new OrgUnit{ Code = "07003", CurrentName = CreateOrgName("กรมชลประทาน","Department of Irrigation")},
                    new OrgUnit{ Code = "07004", CurrentName = CreateOrgName("กรมตรวจบัญชีสหกรณ์","Department of Cooperative Auditing")},
                    new OrgUnit{ Code = "07005", CurrentName = CreateOrgName("กรมประมง","Department of Fisheries")},
                    new OrgUnit{ Code = "07006", CurrentName = CreateOrgName("กรมปศุสัตว์","Department of Livestock Development")},
                    new OrgUnit{ Code = "07008", CurrentName = CreateOrgName("กรมพัฒนาที่ดิน","land Development Department")},
                    new OrgUnit{ Code = "07009", CurrentName = CreateOrgName("กรมวิชาการเกษตร","Department of Agriculture")},
                    new OrgUnit{ Code = "07011", CurrentName = CreateOrgName("กรมส่งเสริมการเกษตร","Department of Agricultural Extension")},
                    new OrgUnit{ Code = "07012", CurrentName = CreateOrgName("กรมส่งเสริมสหกรณ์","Cooperatives Promotion Department")},
                    new OrgUnit{ Code = "07013", CurrentName = CreateOrgName("สำนักงานการปฏิรูปที่ดินเพื่อเกษตรกรรม","Agricultural Land Reform Office")},
                    new OrgUnit{ Code = "07014", CurrentName = CreateOrgName("สำนักงานมาตรฐานสินค้าเกษตรและอาหารแห่งชาติ","National Bureau of Agricultural Commodity and Food Standards.")},
                    new OrgUnit{ Code = "07015", CurrentName = CreateOrgName("สำนักงานเศรษฐกิจการเกษตร","Office of Agricultural Economics")},
                    new OrgUnit{ Code = "07017", CurrentName = CreateOrgName("สถาบันวิจัยและพัฒนาพื้นที่สูง(องค์การมหาชน)","Research and development at high altitudes (ITD)")},
                    new OrgUnit{ Code = "07018", CurrentName = CreateOrgName("กรมการข้าว","Department of rice")},
                    new OrgUnit{ Code = "07019", CurrentName = CreateOrgName("สำนักงานพิพิธภัณฑ์เกษตรเฉลิมพระเกียรติพระบาทสมเด็จพระเจ้าอยู่หัว(องค์การมหาชน)","Office of Agricultural Museum in honor of the King (ITD).")},
                    new OrgUnit{ Code = "07020", CurrentName = CreateOrgName("กรมหม่อนไหม","Department of Sericulture")},
                    new OrgUnit{ Code = "07021", CurrentName = CreateOrgName("กรมฝนหลวงและการบินเกษตร","Rain and Aviation Department of Agriculture.")},
                }
            );
        }

        private void Create08000()
        {
            PersistGovernmentUnit
            (
                "08000", CreateOrgName("กระทรวงคมนาคม", "Ministry of Transport"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "08001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","")},
                    new OrgUnit{ Code = "08002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงคมนาคม","")},
                    new OrgUnit{ Code = "08003", CurrentName = CreateOrgName("กรมเจ้าท่า","")},
                    new OrgUnit{ Code = "08004", CurrentName = CreateOrgName("กรมการขนส่งทางบก","")},
                    new OrgUnit{ Code = "08005", CurrentName = CreateOrgName("กรมการบินพลเรือน","")},
                    new OrgUnit{ Code = "08006", CurrentName = CreateOrgName("กรมทางหลวง","")},
                    new OrgUnit{ Code = "08007", CurrentName = CreateOrgName("กรมทางหลวงชนบท","")},
                    new OrgUnit{ Code = "08008", CurrentName = CreateOrgName("สำนักงานนโยบายและแผนการขนส่งและจราจร","")},
                }
            );
        }

        private void Create09000()
        {
            PersistGovernmentUnit
            (
                "09000", CreateOrgName("กระทรวงทรัพยากรธรรมชาติและสิ่งแวดล้อม", "Ministry of Natural Resources and Environment"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "09001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "09002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงทรัพยากรธรรมชาติและสิ่งแวดล้อม","Ministry of Natural Resources and Environment")},
                    new OrgUnit{ Code = "09003", CurrentName = CreateOrgName("กรมควบคุมมลพิษ","Pollution Control Department")},
                    new OrgUnit{ Code = "09004", CurrentName = CreateOrgName("กรมทรัพยากรทางทะเลและชายฝั่ง","Department of Marine and Coastal")},
                    new OrgUnit{ Code = "09005", CurrentName = CreateOrgName("กรมทรัพยากรธรณี","Department of Mineral Resources")},
                    new OrgUnit{ Code = "09006", CurrentName = CreateOrgName("กรมทรัพยากรน้ำ","Department of Water Resources")},
                    new OrgUnit{ Code = "09007", CurrentName = CreateOrgName("กรมทรัพยากรน้ำบาดาล","Department of Groundwater Resources")},
                    new OrgUnit{ Code = "09008", CurrentName = CreateOrgName("กรมส่งเสริมคุณภาพสิ่งแวดล้อม","Department of Environmental Quality Promotion")},
                    new OrgUnit{ Code = "09009", CurrentName = CreateOrgName("กรมอุทยานแห่งชาติ สัตว์ป่า และพันธุ์พืช","National Park, Wildlife and Plant Conservation.")},
                    new OrgUnit{ Code = "09011", CurrentName = CreateOrgName("สำนักงานนโยบายและแผนทรัพยากรธรรมชาติและสิ่งแวดล้อม","Natural Resources and Environmental Policy and Planning Office.")},
                    new OrgUnit{ Code = "09012", CurrentName = CreateOrgName("กรมป่าไม้","Department of Forestry")},
                    new OrgUnit{ Code = "09013", CurrentName = CreateOrgName("สำนักงานพัฒนาเศรษฐกิจจากฐานชีวภาพ(องค์การมหาชน)","Biodiversity-Based Economy Development Office (Public Organization).")},
                    new OrgUnit{ Code = "09014", CurrentName = CreateOrgName("องค์การบริหารจัดการก๊าซเรือนกระจก(องค์การมหาชน)","Greenhouse Gas Management Organization (Public Organization).")},
                }
            );
        }

        private void Create11000()
        {
            PersistGovernmentUnit
            (
                "11000", CreateOrgName("กระทรวงเทคโนโลยีสารสนเทศและการสื่อสาร", "Ministry of Information and Communication Technology"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "11001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "11002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงเทคโนโลยีสารสนเทศและการสื่อสาร","Ministry of information and communications technology.")},
                    new OrgUnit{ Code = "11003", CurrentName = CreateOrgName("กรมไปรษณีย์โทรเลข","Post and Telegraph Department")},
                    new OrgUnit{ Code = "11004", CurrentName = CreateOrgName("กรมอุตุนิยมวิทยา","Meteorological Department")},
                    new OrgUnit{ Code = "11005", CurrentName = CreateOrgName("สำนักงานสถิติแห่งชาติ","National Statistical Office")},
                    new OrgUnit{ Code = "11006", CurrentName = CreateOrgName("สำนักงานส่งเสริมอุตสาหกรรมซอฟต์แวร์แห่งชาติ(องค์การมหาชน)","National Software Industry Promotion Agency (Public Organization).")},
                    new OrgUnit{ Code = "11007", CurrentName = CreateOrgName("สำนักงานพัฒนาธุรกรรมทางอิเล็กทรอนิกส์(องค์การมหาชน)","Electronic Transactions Development Agency (ITD).")},
                    new OrgUnit{ Code = "11008", CurrentName = CreateOrgName("สำนักงานรัฐบาลอิเล็กทรอนิกส์(องค์การมหาชน)","Electronic Government Agency (Public Organization).")},
                }
            );
        }

        private void Create12000()
        {
            PersistGovernmentUnit
            (
                "12000", CreateOrgName("กระทรวงพลังงาน", "Ministry of Foreign Energy"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "12001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "12002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงพลังงาน","Ministry of Energy")},
                    new OrgUnit{ Code = "12003", CurrentName = CreateOrgName("กรมเชื้อเพลิงธรรมชาติ","DMF")},
                    new OrgUnit{ Code = "12004", CurrentName = CreateOrgName("กรมธุรกิจพลังงาน","The Department of Energy")},
                    new OrgUnit{ Code = "12005", CurrentName = CreateOrgName("กรมพัฒนาพลังงานทดแทนและอนุรักษ์พลังงาน","Department of Renewable Energy and Energy Conservation.")},
                    new OrgUnit{ Code = "12006", CurrentName = CreateOrgName("สำนักงานนโยบายและแผนพลังงาน","Energy Policy and Planning Office")},
                    new OrgUnit{ Code = "12007", CurrentName = CreateOrgName("สถาบันบริหารกองทุนพลังงาน(องค์การมหาชน)","The Energy Fund Administration Institute (Public Organization) ")},
                }
            );
        }

        private void Create13000()
        {
            PersistGovernmentUnit
            (
                "13000", CreateOrgName("กระทรวงพาณิชย์", "Ministry of Commerce"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "13001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "13002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงพาณิชย์","Ministry of Commerce")},
                    new OrgUnit{ Code = "13003", CurrentName = CreateOrgName("กรมการค้าต่างประเทศ","Department of Foreign Trade")},
                    new OrgUnit{ Code = "13004", CurrentName = CreateOrgName("กรมการค้าภายใน","Department of Internal Trade")},
                    new OrgUnit{ Code = "13005", CurrentName = CreateOrgName("กรมการประกันภัย","Department of Insurance")},
                    new OrgUnit{ Code = "13006", CurrentName = CreateOrgName("กรมเจรจาการค้าระหว่างประเทศ","Department of Trade Negotiations")},
                    new OrgUnit{ Code = "13007", CurrentName = CreateOrgName("กรมทรัพย์สินทางปัญญา","Department of Intellectual Property")},
                    new OrgUnit{ Code = "13008", CurrentName = CreateOrgName("กรมพัฒนาธุรกิจการค้า","Department of Business Development")},
                    new OrgUnit{ Code = "13009", CurrentName = CreateOrgName("กรมส่งเสริมการค้าระหว่างประเทศ","Department of Trade")},
                    new OrgUnit{ Code = "13011", CurrentName = CreateOrgName("ศูนย์ส่งเสริมศิลปาชีพระหว่างประเทศ(องค์การมหาชน)","Promotion Center Foundation International (ITD).")},
                    new OrgUnit{ Code = "13012", CurrentName = CreateOrgName("สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ(องค์การมหาชน)","National Jewelry Institute (ITD).")},
                }
            );
        }

        private void Create14000()
        {
            PersistGovernmentUnit
            (
                "14000", CreateOrgName("ทบวงมหาวิทยาลัย", ""),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "14010", CurrentName = CreateOrgName("สำนักงานปลัดทบวงมหาวิทยาลัย", "")}
                }
            );
        }

        private void Create15000()
        {
            PersistGovernmentUnit
            (
                "15000", CreateOrgName("กระทรวงมหาดไทย", "Ministry of Interior"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "15001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "15002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงมหาดไทย","Ministry of Interior")},
                    new OrgUnit{ Code = "15003", CurrentName = CreateOrgName("กรมการปกครอง","Department of Local Administration")},
                    new OrgUnit{ Code = "15004", CurrentName = CreateOrgName("กรมการพัฒนาชุมชน","Community Development Department")},
                    new OrgUnit{ Code = "15005", CurrentName = CreateOrgName("กรมที่ดิน","Department of Lands")},
                    new OrgUnit{ Code = "15006", CurrentName = CreateOrgName("กรมป้องกันและบรรเทาสาธารณภัย","Department of Disaster Prevention and Mitigation")},
                    new OrgUnit{ Code = "15007", CurrentName = CreateOrgName("กรมโยธาธิการและผังเมือง","Department of Public Works and Town Planning")},
                    new OrgUnit{ Code = "15008", CurrentName = CreateOrgName("กรมส่งเสริมการปกครองท้องถิ่น","Department of Local Administration")},
                    new OrgUnit{ Code = "15009", CurrentName = CreateOrgName("กรุงเทพมหานคร","Bangkok")},
                    new OrgUnit{ Code = "15011", CurrentName = CreateOrgName("เมืองพัทยา","Pattaya City")},                    
                }
            );
        }

        private void Create16000()
        {
            PersistGovernmentUnit
            (
                "16000", CreateOrgName("กระทรวงยุติธรรม", "Ministry of Justice"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "16001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "16002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงยุติธรรม","Ministry of Justice")},
                    new OrgUnit{ Code = "16003", CurrentName = CreateOrgName("กรมคุมประพฤติ","Department of Probation")},
                    new OrgUnit{ Code = "16004", CurrentName = CreateOrgName("กรมคุ้มครองสิทธิและเสรีภาพ","Department of Rights and Liberties Protection")},
                    new OrgUnit{ Code = "16005", CurrentName = CreateOrgName("กรมบังคับคดี","Legal Execution Department")},
                    new OrgUnit{ Code = "16006", CurrentName = CreateOrgName("กรมพินิจและคุ้มครองเด็กและเยาวชน","Department of Juvenile Observation and Protection.")},
                    new OrgUnit{ Code = "16007", CurrentName = CreateOrgName("กรมราชทัณฑ์","Department of Corrections")},
                    new OrgUnit{ Code = "16008", CurrentName = CreateOrgName("กรมสอบสวนคดีพิเศษ","Department of Special Investigation")},
                    new OrgUnit{ Code = "16009", CurrentName = CreateOrgName("สำนักงานกิจการยุติธรรม","Office of Justice")},
                    new OrgUnit{ Code = "16010", CurrentName = CreateOrgName("สถาบันนิติวิทยาศาสตร์","forensic Institute")},
                    new OrgUnit{ Code = "16011", CurrentName = CreateOrgName("สำนักงานคณะกรรมการป้องกันและปราบปรามยาเสพติด","Commission Against Drugs.")},
                    new OrgUnit{ Code = "16012", CurrentName = CreateOrgName("สำนักงานคณะกรรมการการป้องกันและปราบปรามการทุจริตในภาครัฐ","Commission Against Corruption in the public sector.")},
                    new OrgUnit{ Code = "16013", CurrentName = CreateOrgName("สถาบันเพื่อการยุติธรรมแห่งประเทศไทย(องค์การมหาชน)","The Institute for Justice of Thailand (Public Organization).")},
                    new OrgUnit{ Code = "16014", CurrentName = CreateOrgName("สถาบันอนุญาโตตุลาการ","Arbitration Institute")},
                }
            );
        }

        private void Create17000()
        {
            PersistGovernmentUnit
            (
                "17000", CreateOrgName("กระทรวงแรงงาน", "Ministry of Labour"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "17001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "17002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงแรงงาน","Ministry of Labor")},
                    new OrgUnit{ Code = "17003", CurrentName = CreateOrgName("กรมการจัดหางาน","Department of Employment")},
                    new OrgUnit{ Code = "17004", CurrentName = CreateOrgName("กรมพัฒนาฝีมือแรงงาน","Department of Skill Development")},
                    new OrgUnit{ Code = "17005", CurrentName = CreateOrgName("กรมสวัสดิการและคุ้มครองแรงงาน","Department of Labor Protection and Welfare")},
                    new OrgUnit{ Code = "17006", CurrentName = CreateOrgName("สำนักงานประกันสังคม","Social Security Office")},          
                }
            );
        }

        private void Create18000()
        {
            PersistGovernmentUnit
            (
                "18000", CreateOrgName("กระทรวงวัฒนธรรม", "Ministry of Culture"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "18001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "18002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงวัฒนธรรม","Ministry of Culture")},
                    new OrgUnit{ Code = "18003", CurrentName = CreateOrgName("กรมการศาสนา","Department of Religious Affairs")},
                    new OrgUnit{ Code = "18004", CurrentName = CreateOrgName("กรมศิลปากร","Fine Arts Department")},
                    new OrgUnit{ Code = "18005", CurrentName = CreateOrgName("กรมส่งเสริมวัฒนธรรม","Department of Cultural Promotion")},
                    new OrgUnit{ Code = "18006", CurrentName = CreateOrgName("สำนักงานศิลปวัฒนธรรมร่วมสมัย","Office of Contemporary Art and Culture")},
                    new OrgUnit{ Code = "18007", CurrentName = CreateOrgName("ศูนย์มานุษยวิทยาสิรินธร(องค์การมหาชน)","Sirindhorn Anthropology Centre (Public Organization.)")},
                    new OrgUnit{ Code = "18008", CurrentName = CreateOrgName("สถาบันบัณฑิตพัฒนศิลป์","Institute of Arts")},
                    new OrgUnit{ Code = "18009", CurrentName = CreateOrgName("หอภาพยนตร์(องค์การมหาชน)","Film Archive (Public Organization) ")},
                    new OrgUnit{ Code = "18010", CurrentName = CreateOrgName("ศูนย์คุณธรรม(องค์การมหาชน)","Center for Public Integrity (ITD)")},
                    new OrgUnit{ Code = "18010", CurrentName = CreateOrgName("ศูนย์ความเป็นเลิศด้านชีววิทยาศาสตร์ (องค์การมหาชน)","Center of Excellence for Life Sciences. (ITD)")},

                }
            );
        }

        private void Create19000()
        {
            PersistGovernmentUnit
            (
                "19000", CreateOrgName("กระทรวงวิทยาศาสตร์และเทคโนโลยี", "Ministry of Science and Technology"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "19001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "19002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงวิทยาศาสตร์และเทคโนโลยี","Ministry of Science and Technology")},
                    new OrgUnit{ Code = "19003", CurrentName = CreateOrgName("กรมวิทยาศาสตร์บริการ","Department of Science Service")},
                    new OrgUnit{ Code = "19004", CurrentName = CreateOrgName("สำนักงานปรมาณูเพื่อสันติ","Office of Atoms for Peace")},
                    new OrgUnit{ Code = "19005", CurrentName = CreateOrgName("สำนักงานพัฒนาวิทยาศาตร์และเทคโนโลยีแห่งชาติ","Office of Science and Technology.")},
                    new OrgUnit{ Code = "19006", CurrentName = CreateOrgName("สำนักงานพัฒนาเทคโนโลยีอวกาศและภูมิสารสนเทศ(องค์การมหาชน)","Geo-Informatics and Space Technology Development Agency (ITD).")},
                    new OrgUnit{ Code = "19008", CurrentName = CreateOrgName("สถาบันเทคโนโลยีนิวเคลียร์แห่งชาติ(องค์การมหาชน)","Institute of Nuclear Technology (Public Organization).")},
                    new OrgUnit{ Code = "19009", CurrentName = CreateOrgName("สำนักงานคณะกรรมการนโยบายวิทยาศาสตร์ เทคโนโลยีและนวัตกรรมแห่งชาติ","Science Policy Office Technology and Innovation")},
                    new OrgUnit{ Code = "19010", CurrentName = CreateOrgName("สถาบันวิจัยแสงซินโครตรอน(องค์การมหาชน)","Electron Synchrotron Light Research Institute (Public Organization).")},
                    new OrgUnit{ Code = "19011", CurrentName = CreateOrgName("สถาบันวิจัยดาราศาสตร์แห่งชาติ(องค์การมหาชน)","Astronomy (ITD).")},
                    new OrgUnit{ Code = "19012", CurrentName = CreateOrgName("สถาบันสารสนเทศทรัพยากรน้ำและการเกษตร(องค์การมหาชน)","Hydro and Agro Informatics Institute (ITD).")},
                    new OrgUnit{ Code = "19013", CurrentName = CreateOrgName("สำนักงานนวัตกรรมแห่งชาติ(องค์การมหาชน)","National Innovation Agency (Public Organization).")},
                    new OrgUnit{ Code = "19014", CurrentName = CreateOrgName("ศูนย์ความเป็นเลิศด้านชีววิทยาศาสตร์(องค์การมหาชน)","Center of Excellence for Life Sciences (ITD).")},
                }
            );
        }

        private void Create20000()
        {
            PersistGovernmentUnit
            (
                "20000", CreateOrgName("กระทรวงศึกษาธิการ", "Ministry of Education"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "20001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                    new OrgUnit{ Code = "20002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงศึกษาธิการ","Ministry of Education")},
                    new OrgUnit{ Code = "20003", CurrentName = CreateOrgName("สำนักงานเลขาธิการสภาการศึกษา","Office of the Education Council")},
                    new OrgUnit{ Code = "20004", CurrentName = CreateOrgName("สำนักงานคณะกรรมการการศึกษาขั้นพื้นฐาน","Office of the Basic Education.")},
                    new OrgUnit{ Code = "20005", CurrentName = CreateOrgName("สำนักงานคณะกรรมการการอุดมศึกษา","Commission on Higher Education")},
                    new OrgUnit{ Code = "20006", CurrentName = CreateOrgName("สำนักงานคณะกรรมการการอาชีวศึกษา","Office of Vocational Education Commission")},
                    new OrgUnit{ Code = "20102", CurrentName = CreateOrgName("มหาวิทยาลัยเกษตรศาสตร์","Kasetsart University")},
                    new OrgUnit{ Code = "20103", CurrentName = CreateOrgName("มหาวิทยาลัยขอนแก่น","Khon Kaen University")},
                    new OrgUnit{ Code = "20106", CurrentName = CreateOrgName("มหาวิทยาลัยธรรมศาสตร์","Thammasat University")},
                    new OrgUnit{ Code = "20107", CurrentName = CreateOrgName("มหาวิทยาลัยนเรศวร","Naresuan University")},
                    new OrgUnit{ Code = "20109", CurrentName = CreateOrgName("มหาวิทยาลัยมหาสารคาม","Mahasarakham University")},
                    new OrgUnit{ Code = "20111", CurrentName = CreateOrgName("มหาวิทยาลัยแม่โจ้","Mae Jo University")},
                    new OrgUnit{ Code = "20112", CurrentName = CreateOrgName("มหาวิทยาลัยรามคำแหง","Ramkhamhaeng University")},
                    new OrgUnit{ Code = "20113", CurrentName = CreateOrgName("มหาวิทยาลัยศรีนครินทรวิโรฒ","Srinakharinwirot University")},
                    new OrgUnit{ Code = "20114", CurrentName = CreateOrgName("มหาวิทยาลัยศิลปากร","Silpakorn University")},
                    new OrgUnit{ Code = "20115", CurrentName = CreateOrgName("มหาวิทยาลัยสงขลานครินทร์","Prince of Songkhla University")},
                    new OrgUnit{ Code = "20116", CurrentName = CreateOrgName("มหาวิทยาลัยสุโขทัยธรรมาธิราช","Sukhothai Thammathirat University.")},
                    new OrgUnit{ Code = "20117", CurrentName = CreateOrgName("มหาวิทยาลัยอุบลราชธานี","Ubon Ratchathani University")},
                    new OrgUnit{ Code = "20120", CurrentName = CreateOrgName("สถาบันบัณฑิตพัฒนบริหารศาสตร์","Institute of Development Administration")},
                    new OrgUnit{ Code = "20122", CurrentName = CreateOrgName("มหาวิทยาลัยมหาจุฬาลงกรณราชวิทยาลัย","Chulalongkorn University's College.")},
                    new OrgUnit{ Code = "20123", CurrentName = CreateOrgName("มหาวิทยาลัยมหามกุฏราชวิทยาลัย","Mahamakutrajawittayalai University College.")},
                    new OrgUnit{ Code = "20124", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏกาญจนบุรี","Kanchanaburi Rajabhat University")},
                    new OrgUnit{ Code = "20125", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏกาฬสินธุ์","University Kalasin")},
                    new OrgUnit{ Code = "20126", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏกำแพงเพชร","Kamphaeng Phet Rajabhat University")},
                    new OrgUnit{ Code = "20127", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏจันทรเกษม","Chandrakasem Rajabhat University")},
                    new OrgUnit{ Code = "20128", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏชัยภูมิ","Chaiyaphum Rajabhat University")},
                    new OrgUnit{ Code = "20129", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏเชียงราย","Chiang Rai Rajabhat University")},
                    new OrgUnit{ Code = "20130", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏเชียงใหม่","Chiang Mai Rajabhat University")},
                    new OrgUnit{ Code = "20131", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏเทพสตรี","University Thepsatri")},
                    new OrgUnit{ Code = "20132", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏธนบุรี","Thonburi Rajabhat University")},
                    new OrgUnit{ Code = "20133", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏนครปฐม","Nakhon Pathom Rajabhat University")},
                    new OrgUnit{ Code = "20135", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏนครราชสีมา","Nakhon Ratchasima Rajabhat University")},
                    new OrgUnit{ Code = "20136", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏนครศรีธรรมราช","Nakhon Si Thammarat Rajabhat University")},
                    new OrgUnit{ Code = "20137", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏนครสวรรค์","Nakhon Sawan Rajabhat University")},
                    new OrgUnit{ Code = "20138", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏบ้านสมเด็จเจ้าพระยา","University Bansomdejchaopraya")},
                    new OrgUnit{ Code = "20139", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏบุรีรัมย์","Buriram Rajabhat University")},
                    new OrgUnit{ Code = "20140", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏพระนคร","Phranakhon Rajabhat University")},
                    new OrgUnit{ Code = "20141", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏพระนครศรีอยุธยา","Phra Nakhon Si Ayutthaya Rajabhat University")},
                    new OrgUnit{ Code = "20142", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏพิบูลสงคราม","University Phiboonsongkram")},
                    new OrgUnit{ Code = "20143", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏเพชรบุรี","Phetchaburi Rajabhat University")},
                    new OrgUnit{ Code = "20144", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏวไลยอลงกรณ์ ในพระบรมราชูปถัมภ์จังหวัดปทุมธานี","University Valaya Nationals Thani Province")},
                    new OrgUnit{ Code = "20145", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏเพชรบูรณ์","Phetchabun Rajabhat University")},
                    new OrgUnit{ Code = "20146", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏภูเก็ต","Phuket Rajabhat University")},
                    new OrgUnit{ Code = "20147", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏมหาสารคาม","Rajabhat Mahasarakham University")},
                    new OrgUnit{ Code = "20148", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏยะลา","Yala Rajabhat University")},
                    new OrgUnit{ Code = "20149", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏราชนครินทร์","Rajabhat Rajanagarindra University")},
                    new OrgUnit{ Code = "20150", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏร้อยเอ็ด","Roi Et Rajabhat University")},
                    new OrgUnit{ Code = "20151", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏรำไพพรรณี","University Rambhaibarni")},
                    new OrgUnit{ Code = "20152", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏเลย","Loei Rajabhat University")},
                    new OrgUnit{ Code = "20153", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏลำปาง","Lampang Rajabhat University")},
                    new OrgUnit{ Code = "20154", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏศรีสะเกษ","Sisaket Rajabhat University")},
                    new OrgUnit{ Code = "20155", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏสกลนคร","Sakon Nakhon Rajabhat University")},
                    new OrgUnit{ Code = "20156", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏสงขลา","Songkhla Rajabhat University")},
                    new OrgUnit{ Code = "20157", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏสวนดุสิต","Suan Dusit Rajabhat University")},
                    new OrgUnit{ Code = "20158", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏสวนสุนันทา","Suan Sunandha Rajabhat University")},
                    new OrgUnit{ Code = "20159", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏสุราษฎร์ธานี","Surat Thani Rajabhat University")},
                    new OrgUnit{ Code = "20160", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏสุรินทร์","Surindra Rajabhat University")},
                    new OrgUnit{ Code = "20161", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏหมู่บ้านจอมบึง","University Village, Chom Bung")},
                    new OrgUnit{ Code = "20162", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏอุดรธานี","Udon Thani Rajabhat University")},
                    new OrgUnit{ Code = "20163", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏอุตรดิตถ์","Uttaradit Rajabhat University")},
                    new OrgUnit{ Code = "20164", CurrentName = CreateOrgName("มหาวิทยาลัยราชภัฏอุบลราชธานี","Ubon Ratchathani Rajabhat University")},
                    new OrgUnit{ Code = "20165", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลธัญบุรี","Rajamangala University of Technology")},
                    new OrgUnit{ Code = "20166", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลกรุงเทพ","Rajamangala University of Technology Krungthep")},
                    new OrgUnit{ Code = "20167", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก","Rajamangala University of Technology of East")},
                    new OrgUnit{ Code = "20168", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลพระนคร","Rajamangala University of Technology Phra Nakhon")},
                    new OrgUnit{ Code = "20169", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลรัตนโกสินทร์","Rajamangala University of Technology Rattanakosin")},
                    new OrgUnit{ Code = "20170", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลล้านนา","Rajamangala University of Technology Lanna")},
                    new OrgUnit{ Code = "20171", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลศรีวิชัย","Rajamangala University of Technology Srivijaya")},

                    new OrgUnit{ Code = "20172", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลสุวรรณภูมิ","Rajamangala University of Technology")},
                    new OrgUnit{ Code = "20173", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีราชมงคลอีสาน","Rajamangala University of Technology Isan")},
                    new OrgUnit{ Code = "20174", CurrentName = CreateOrgName("สถาบันเทคโนโลยีปทุมวัน","Pathumwan Institute of Technology")},
                    new OrgUnit{ Code = "20175", CurrentName = CreateOrgName("มหาวิทยาลัยนราธิวาสราชนครินทร์","University of Narathiwat")},
                    new OrgUnit{ Code = "20176", CurrentName = CreateOrgName("มหาวิทยาลัยนครพนม","Nakhon Phanom University")},
                    new OrgUnit{ Code = "20301", CurrentName = CreateOrgName("สถาบันส่งเสริมการสอนวิทยาศาสตร์และเทคโนโลยี","Institute for the Promotion of Teaching Science and Technology.")},
                    new OrgUnit{ Code = "20302", CurrentName = CreateOrgName("โรงเรียนมหิดลวิทยานุสรณ์","University School Wittayanusorn")},
                    new OrgUnit{ Code = "20304", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีสุรนารี","Suranaree University of Technology")},
                    new OrgUnit{ Code = "20305", CurrentName = CreateOrgName("มหาวิทยาลัยวลัยลักษณ์","Walailak University")},
                    new OrgUnit{ Code = "20306", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีพระจอมเกล้าธนบุรี","King Mongkut University")},
                    new OrgUnit{ Code = "20307", CurrentName = CreateOrgName("มหาวิทยาลัยแม่ฟ้าหลวง","Mae Fah Luang University")},
                    new OrgUnit{ Code = "20308", CurrentName = CreateOrgName("สถาบันระหว่างประเทศเพื่อการค้าและการพัฒนา(องค์การมหาชน)","The International Institute for Trade and Development (ITD).")},
                    new OrgUnit{ Code = "20309", CurrentName = CreateOrgName("สำนักงานเลขาธิการคุรุสภา","Council Secretariat")},
                    new OrgUnit{ Code = "20310", CurrentName = CreateOrgName("สำนักงานคณะกรรมการส่งเสริมสวัสดิการและสวัสดิภาพครูและบุคลากรทางการศึกษา","Board welfare and the welfare of teachers and education personnel.")},
                    new OrgUnit{ Code = "20311", CurrentName = CreateOrgName("สถาบันทดสอบทางการศึกษาแห่งชาติ(องค์การมหาชน)","The National Institute of Educational Testing Service (Public Organization).")},
                    new OrgUnit{ Code = "20312", CurrentName = CreateOrgName("มหาวิทยาลัยมหิดล","Mahidol University")},
                    new OrgUnit{ Code = "20313", CurrentName = CreateOrgName("มหาวิทยาลัยเทคโนโลยีพระจอมเกล้าพระนครเหนือ","KMUTNB")},
                    new OrgUnit{ Code = "20314", CurrentName = CreateOrgName("มหาวิทยาลัยบูรพา","University of the East")},
                    new OrgUnit{ Code = "20315", CurrentName = CreateOrgName("มหาวิทยาลัยทักษิณ","Thaksin University")},
                    new OrgUnit{ Code = "20316", CurrentName = CreateOrgName("จุฬาลงกรณ์มหาวิทยาลัย","Chulalongkorn University")},
                    new OrgUnit{ Code = "20317", CurrentName = CreateOrgName("มหาวิทยาลัยเชียงใหม่","Chiang Mai University")},
                    new OrgUnit{ Code = "20318", CurrentName = CreateOrgName("สถาบันเทคโนโลยีพระจอมเกล้าเจ้าคุณทหารลาดกระบัง","King Mongkut's Institute of Technology Lat Krabang, Bangkok")},
                    new OrgUnit{ Code = "20319", CurrentName = CreateOrgName("มหาวิทยาลัยพะเยา","University of Phayao")},
                    new OrgUnit{ Code = "20320", CurrentName = CreateOrgName("สถาบันดนตรีกัลยาณิวัฒนา","Academy of Music Kalayaniwattana")},
                }
            );
        }

        private void Create21000()
        {
            PersistGovernmentUnit
             (
                 "21000", CreateOrgName("กระทรวงสาธารณสุข", "Ministry of Public Health"),
                 new List<OrgUnit>
                 {
                     new OrgUnit{ Code = "21001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี","Cabinet Office")},
                     new OrgUnit{ Code = "21002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงสาธารณสุข","Ministry of Health")},
                     new OrgUnit{ Code = "21003", CurrentName = CreateOrgName("กรมการแพทย์","Medical Department")},
                     new OrgUnit{ Code = "21004", CurrentName = CreateOrgName("กรมควบคุมโรค","Disease Control Department")},
                     new OrgUnit{ Code = "21005", CurrentName = CreateOrgName("กรมพัฒนาการแพทย์แผนไทยและการแพทย์ทางเลือก","Thailand Department of Developmental Medicine and Alternative Medicine.")},
                     new OrgUnit{ Code = "21006", CurrentName = CreateOrgName("กรมวิทยาศาสตร์การแพทย์","Department of Medical Science")},
                     new OrgUnit{ Code = "21007", CurrentName = CreateOrgName("กรมสนับสนุนบริการสุขภาพ","Department of Health Service Support")},
                     new OrgUnit{ Code = "21008", CurrentName = CreateOrgName("กรมสุขภาพจิต","Department of Mental Health")},
                     new OrgUnit{ Code = "21009", CurrentName = CreateOrgName("กรมอนามัย","Department of Health")},
                     new OrgUnit{ Code = "21010", CurrentName = CreateOrgName("สำนักงานคณะกรรมการอาหารและยา","Food and Drug Administration")},
                     new OrgUnit{ Code = "21011", CurrentName = CreateOrgName("สถาบันวิจัยระบบสาธารณสุข","Health Systems Research Institute")},
                     new OrgUnit{ Code = "21013", CurrentName = CreateOrgName("สำนักงานหลักประกันสุขภาพแห่งชาติ","National Health Security Office")},
                     new OrgUnit{ Code = "21014", CurrentName = CreateOrgName("สถาบันการแพทย์ฉุกเฉินแห่งชาติ","National Institute of Medical Emergency")},
                     new OrgUnit{ Code = "21015", CurrentName = CreateOrgName("สถาบันรับรองคุณภาพสถานพยาบาล(องค์การมหาชน)","Institute of Nursing Certificate (ITD).")},
                     new OrgUnit{ Code = "21016", CurrentName = CreateOrgName("สถาบันวัคซีนแห่งชาติ (องค์การมหาชน)","National Vaccine Institute (ITD)")},
                     new OrgUnit{ Code = "21012", CurrentName = CreateOrgName("โรงพยาบาลบ้านแพ้ว (องค์การมหาชน)","Banphaeo hospital (ITD)")},
                 }
             );
        }

        private void Create22000()
        {
            PersistGovernmentUnit
            (
                "22000", CreateOrgName("กระทรวงอุตสาหกรรม", "Ministry of Industry"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "22001", CurrentName = CreateOrgName("สำนักงานรัฐมนตรี", "Cabinet Office")},
                    new OrgUnit{ Code = "22002", CurrentName = CreateOrgName("สำนักงานปลัดกระทรวงอุตสาหกรรม", "")},
                    new OrgUnit{ Code = "22003", CurrentName = CreateOrgName("กรมโรงงานอุตสาหกรรม", "Ministry of Industry")},
                    new OrgUnit{ Code = "22004", CurrentName = CreateOrgName("กรมส่งเสริมอุตสาหกรรม", "Department of Industrial Promotion")},
                    new OrgUnit{ Code = "22005", CurrentName = CreateOrgName("กรมอุตสาหกรรมพื้นฐานและการเหมืองแร่", "Department of Primary Industries and Mines.")},
                    new OrgUnit{ Code = "22006", CurrentName = CreateOrgName("สำนักงานคณะกรรมการอ้อยและน้ำตาลทราย", "Office of the Cane and Sugar.")},
                    new OrgUnit{ Code = "22007", CurrentName = CreateOrgName("สำนักงานมาตรฐานผลิตภัณฑ์อุตสาหกรรม", "Thai Industrial Standards Institute")},
                    new OrgUnit{ Code = "22008", CurrentName = CreateOrgName("สำนักงานเศรษฐกิจอุตสาหกรรม", "Office of Industrial Economics")},
                    new OrgUnit{ Code = "22009", CurrentName = CreateOrgName("สำนักงานคณะกรรมการส่งเสริมการลงทุน", "Office of the Board of Investment")},
                }
            );
        }

        private void Create25000()
        {
            PersistGovernmentUnit
            (
                "25000", CreateOrgName("หน่วยงานขององค์กรตามรัฐธรรมนูญ และหน่วยงานอิสระตามรัฐธรรมนูญ", ""),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "25001", CurrentName = CreateOrgName("สำนักราชเลขาธิการ","Office of His Majesty's Principal Private Secretary")},
                    new OrgUnit{ Code = "25002", CurrentName = CreateOrgName("สำนักพระราชวัง","Bureau")},
                    new OrgUnit{ Code = "25003", CurrentName = CreateOrgName("สำนักงานพระพุทธศาสนาแห่งชาติ","Office of National Buddhism")},
                    new OrgUnit{ Code = "25004", CurrentName = CreateOrgName("สำนักงานคณะกรรมการพิเศษเพื่อประสานงานโครงการอันเนื่องมาจากพระราชดำริ","Office of the Special Coordinator for the project due to the initiative.")},
                    new OrgUnit{ Code = "25005", CurrentName = CreateOrgName("สำนักงานคณะกรรมการวิจัยแห่งชาติ","National Research Council")},
                    new OrgUnit{ Code = "25006", CurrentName = CreateOrgName("ราชบัณฑิตยสถาน","Royal Academy")},
                    new OrgUnit{ Code = "25007", CurrentName = CreateOrgName("สำนักงานตำรวจแห่งชาติ","Royal Thai Police")},
                    new OrgUnit{ Code = "25008", CurrentName = CreateOrgName("สำนักงานป้องกันและปราบปรามการฟอกเงิน","Anti-money laundering.")},
                    new OrgUnit{ Code = "25009", CurrentName = CreateOrgName("สำนักงานอัยการสูงสุด","Attorney General's Office")},
                    new OrgUnit{ Code = "25010", CurrentName = CreateOrgName("สำนักงานเลขาธิการวุฒิสภา","Secretariat of the Senate")},
                    new OrgUnit{ Code = "25011", CurrentName = CreateOrgName("สำนักงานเลขาธิการสภาผู้แทนราษฎร","Secretariat of the House of Representatives")},
                    new OrgUnit{ Code = "25012", CurrentName = CreateOrgName("สถาบันพระปกเกล้า","King Prajadhipok's Institute")},
                    new OrgUnit{ Code = "25015", CurrentName = CreateOrgName("สำนักงานสภาที่ปรึกษาเศรษฐกิจและสังคมแห่งชาติ","The Office of the National Economic and Social Advisory Council.")},
                    new OrgUnit{ Code = "26011", CurrentName = CreateOrgName("สำนักงานคณะกรรมการปฏิรูปกฎหมาย","Law Reform Commission")},
                }
            );
        }

        private void Create26000()
        {
            PersistGovernmentUnit
            (
                "26000", CreateOrgName("หน่วยงานอิสระตามรัฐธรรมนูญ", ""),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "26001", CurrentName = CreateOrgName("สำนักงานคณะกรรมการการเลือกตั้ง", "Commission on Elections")},
                    new OrgUnit{ Code = "26002", CurrentName = CreateOrgName("สำนักงานศาลรัฐธรรมนูญ", "Office of Constitutional Court")},
                    new OrgUnit{ Code = "26003", CurrentName = CreateOrgName("สำนักงานผู้ตรวจการแผ่นดิน", "The Office of the Ombudsman")},
                    new OrgUnit{ Code = "26004", CurrentName = CreateOrgName("สำนักงานศาลปกครอง","Administrative offices")},
                    new OrgUnit{ Code = "26005", CurrentName = CreateOrgName("สำนักงานคณะกรรมการป้องกันและปราบปรามการทุจริตแห่งชาติ","The Office of the National Anti-Corruption.")},
                    new OrgUnit{ Code = "26006", CurrentName = CreateOrgName("สำนักงานการตรวจเงินแผ่นดิน", "Office of the Auditor General of Thailand")},
                    new OrgUnit{ Code = "26007", CurrentName = CreateOrgName("สำนักงานคณะกรรมการสิทธิมนุษยชนแห่งชาติ", "National Human Rights Commission.")},
                    new OrgUnit{ Code = "26008", CurrentName = CreateOrgName("สำนักงานศาลยุติธรรม", "The Judiciary")},
                }
            );
        }

        private void Create50000()
        {
            PersistGovernmentUnit
            (
                "50000", CreateOrgName("รัฐวิสาหกิจ", "State Enterprises"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "50312", CurrentName = CreateOrgName("การรถไฟแห่งประเทศไทย","State Railway of Thailand")},
                    new OrgUnit{ Code = "50313", CurrentName = CreateOrgName("การรถไฟฟ้าขนส่งมวลชนแห่งประเทศไทย","Mass Rapid Transit Authority of Thailand")},
                    new OrgUnit{ Code = "50602", CurrentName = CreateOrgName("การประปาส่วนภูมิภาค","The Provincial Waterworks Authority")},
                    new OrgUnit{ Code = "50404", CurrentName = CreateOrgName("การท่องเที่ยวแห่งประเทศไทย","Tourism Authority of Thailand")},
                    new OrgUnit{ Code = "50301", CurrentName = CreateOrgName("การทางพิเศษแห่งประเทศไทย","Expressway Authority of Thailand")},
                    new OrgUnit{ Code = "50101", CurrentName = CreateOrgName("องค์การตลาดเพื่อเกษตรกร","Marketing Organization for Farmers")},
                    new OrgUnit{ Code = "50102", CurrentName = CreateOrgName("องค์การสวนยาง","Rubber Estate Organization")},
                    new OrgUnit{ Code = "50105", CurrentName = CreateOrgName("องค์การอุตสาหกรรมป่าไม้","Forest Industry Organisation")},
                    new OrgUnit{ Code = "50204", CurrentName = CreateOrgName("องค์การเภสัชกรรม","Government Pharmaceutical Organization")},
                    new OrgUnit{ Code = "50302", CurrentName = CreateOrgName("องค์การขนส่งมวลชนกรุงเทพ","Bangkok Mass Transit Authority")},
                    new OrgUnit{ Code = "50604", CurrentName = CreateOrgName("การกีฬาแห่งประเทศไทย","Sports Authority of Thailand")},
                    new OrgUnit{ Code = "50311", CurrentName = CreateOrgName("สถาบันการบินพลเรือน","Civil Aviation")},
                    new OrgUnit{ Code = "50402", CurrentName = CreateOrgName("องค์การคลังสินค้า","Public Warehouse Organization")},
                    new OrgUnit{ Code = "50502", CurrentName = CreateOrgName("องค์การพิพิธภันฑ์วิทยาศาสตร์แห่งชาติ","Organization National Science Museum")},
                    new OrgUnit{ Code = "50503", CurrentName = CreateOrgName("องค์การสวนพฤกษศาสตร์","The Botanical Garden Organization")},
                    new OrgUnit{ Code = "50505", CurrentName = CreateOrgName("การไฟฟ้านครหลวง","Metropolitan Electricity Authority")},
                    new OrgUnit{ Code = "50506", CurrentName = CreateOrgName("การไฟฟ้าส่วนภูมิภาค","Provincial Electricity Authority")},
                    new OrgUnit{ Code = "50510", CurrentName = CreateOrgName("องค์การจัดการน้ำเสีย","Wastewater Management")},
                    new OrgUnit{ Code = "50601", CurrentName = CreateOrgName("การประปานครหลวง","Metropolitan Waterworks Authority")},
                    new OrgUnit{ Code = "50603", CurrentName = CreateOrgName("การเคหะแห่งชาติ","National Housing Authority")},
                    new OrgUnit{ Code = "50605", CurrentName = CreateOrgName("องค์การสวนสัตว์","Zoological Park Organization")},
                    new OrgUnit{ Code = "50703", CurrentName = CreateOrgName("ธนาคารเพื่อการเกษตรและสหกรณ์การเกษตร","The Bank for Agriculture and Agricultural Cooperatives")},
                    new OrgUnit{ Code = "50106", CurrentName = CreateOrgName("สำนักงานกองทุนสงเคราะห์การทำสวนยาง","Office of the Rubber Replanting Aid Fund")},
                    new OrgUnit{ Code = "50201", CurrentName = CreateOrgName("การนิคมอุตสาหกรรมแห่งประเทศไทย","Industrial Estate Authority of Thailand")},
                    new OrgUnit{ Code = "50706", CurrentName = CreateOrgName("ธนาคารเพื่อการส่งออกและนำเข้าแห่งประเทศไทย","The Export-Import Bank of Thailand.")},
                    new OrgUnit{ Code = "50708", CurrentName = CreateOrgName("ธนาคารพัฒนาวิสาหกิจขนาดกลางและขนาดย่อมแห่งประเทศไทย","Small and Medium Enterprise Development Bank of Thailand.")},
                    new OrgUnit{ Code = "50501", CurrentName = CreateOrgName("สถาบันวิจัยวิทยาศาสตร์และเทคโนโลยีแห่งประเทศไทย","Institute of Science and Technology of Thailand")},
                    new OrgUnit{ Code = "50711", CurrentName = CreateOrgName("บรรษัทประกันสินเชื่ออุตสาหกรรมขนาดย่อม","Small Industry Credit Guarantee Corporation")},
                    new OrgUnit{ Code = "50719", CurrentName = CreateOrgName("บริษัท ทีโอที จำกัด (มหาชน)","TOT Public Company Limited (Thailand).")},
                    new OrgUnit{ Code = "50106", CurrentName = CreateOrgName("สำนักงานกองทุนสงเคราะห์การสวนยาง","Office of the Rubber Replanting Aid Fund")},
                    new OrgUnit{ Code = "50706", CurrentName = CreateOrgName("ธนาคารเพื่อการส่งออกและนำเข้าแห่งประเทศไทย","The Export-Import Bank of Thailand.")},
                    new OrgUnit{ Code = "50705", CurrentName = CreateOrgName("ธนาคารอาคารสงเคราะห์","Government Housing Bank")},
                    new OrgUnit{ Code = "50706", CurrentName = CreateOrgName("ธนาคารเพื้อการส่งออกและนำเข้าแห่งประเทศไทย","Bank the purpose of export and import of Thailand.")},
                    new OrgUnit{ Code = "50709", CurrentName = CreateOrgName("ธนาคารอิสลามแห่งประเทศไทย","Islamic Bank of Thailand")},
                    new OrgUnit{ Code = "50306", CurrentName = CreateOrgName("บริษัท ท่าอากาศยานไทย จำกัด (มหาชน)","Airports of Thailand (AOT).")},
                    new OrgUnit{ Code = "50103", CurrentName = CreateOrgName("องค์การสะพานปลา","Fish Marketing Organization")},
                }
            );
        }

        private void Create60000()
        {
            PersistGovernmentUnit
            (
                "60000", CreateOrgName("สภากาชาดไทย", "The Thai Red Cross Society"),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "60002", CurrentName = CreateOrgName("สภากาชาด", "")}
                }
            );
        }

        private void Create70000()
        {
            PersistGovernmentUnit
             (
                 "70000", CreateOrgName("จังหวัดและกลุ่มจังหวัด", ""),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "70011", CurrentName = CreateOrgName("นนทบุรี","Nonthaburi")},
                    new OrgUnit{ Code = "70012", CurrentName = CreateOrgName("ปทุมธานี","Pathum Thani")},
                    new OrgUnit{ Code = "70013", CurrentName = CreateOrgName("พระนครศรีอยุธยา","Phra Nakhon Si Ayutthaya")},
                    new OrgUnit{ Code = "70014", CurrentName = CreateOrgName("สระบุรี","Saraburi")},
                    new OrgUnit{ Code = "70021", CurrentName = CreateOrgName("ชัยนาท","Chai Nat")},
                    new OrgUnit{ Code = "70022", CurrentName = CreateOrgName("ลพบุรี","Lop Buri")},
                    new OrgUnit{ Code = "70023", CurrentName = CreateOrgName("สิงห์บุรี","Sing Buri")},
                    new OrgUnit{ Code = "70024", CurrentName = CreateOrgName("อ่างทอง","Ang Thong")},
                    new OrgUnit{ Code = "70031", CurrentName = CreateOrgName("ฉะเชิงเทรา","Chachoengsao")},
                    new OrgUnit{ Code = "70032", CurrentName = CreateOrgName("ปราจีนบุรี","Prachin Buri")},
                    new OrgUnit{ Code = "70033", CurrentName = CreateOrgName("สระแก้ว","Sa Kaeo")},
                    new OrgUnit{ Code = "70034", CurrentName = CreateOrgName("นครนายก","Nakhon Nayok")},
                    new OrgUnit{ Code = "70035", CurrentName = CreateOrgName("สมุทรปราการ","Samut Prakan")},
                    new OrgUnit{ Code = "70041", CurrentName = CreateOrgName("กาญจนบุรี","Kanchanaburi")},
                    new OrgUnit{ Code = "70042", CurrentName = CreateOrgName("นครปฐม","Nakhon Pathom")},
                    new OrgUnit{ Code = "70043", CurrentName = CreateOrgName("ราชบุรี","Ratchaburi")},
                    new OrgUnit{ Code = "70044", CurrentName = CreateOrgName("สุพรรณบุรี","Suphan Buri")},
                    new OrgUnit{ Code = "70051", CurrentName = CreateOrgName("ประจวบคีรีขันธ์","Prachuap Khiri Khan")},
                    new OrgUnit{ Code = "70052", CurrentName = CreateOrgName("เพชรบุรี","Petchburi")},
                    new OrgUnit{ Code = "70053", CurrentName = CreateOrgName("สมุทรสาคร","Samut Sakhon")},
                    new OrgUnit{ Code = "70054", CurrentName = CreateOrgName("สมุทรสงคราม","Samut Songkhram")},
                    new OrgUnit{ Code = "70061", CurrentName = CreateOrgName("ชุมพร","Chumphon")},
                    new OrgUnit{ Code = "70062", CurrentName = CreateOrgName("สุราษฎร์ธานี","Surat Thani")},
                    new OrgUnit{ Code = "70063", CurrentName = CreateOrgName("นครศรีธรรมราช","Nakhon Si Thammarat")},
                    new OrgUnit{ Code = "70064", CurrentName = CreateOrgName("พัทลุง","Phatthalung")},
                    new OrgUnit{ Code = "70071", CurrentName = CreateOrgName("ระนอง","Ranong")},
                    new OrgUnit{ Code = "70072", CurrentName = CreateOrgName("พังงา","Phangnga")},
                    new OrgUnit{ Code = "70073", CurrentName = CreateOrgName("ภูเก็ต","Phuket")},
                    new OrgUnit{ Code = "70074", CurrentName = CreateOrgName("กระบี่","Krabi")},
                    new OrgUnit{ Code = "70075", CurrentName = CreateOrgName("ตรัง","Trang")},
                    new OrgUnit{ Code = "70081", CurrentName = CreateOrgName("สงขลา","Songkhla")},
                    new OrgUnit{ Code = "70082", CurrentName = CreateOrgName("สตูล","Satun")},
                    new OrgUnit{ Code = "70083", CurrentName = CreateOrgName("ปัตตานี","Pattani")},
                    new OrgUnit{ Code = "70084", CurrentName = CreateOrgName("ยะลา","Yala")},
                    new OrgUnit{ Code = "70085", CurrentName = CreateOrgName("นราธิวาส","Narathiwat")},
                    new OrgUnit{ Code = "70091", CurrentName = CreateOrgName("จันทบุรี","Chanthaburi")},
                    new OrgUnit{ Code = "70092", CurrentName = CreateOrgName("ชลบุรี","Chon Buri")},
                    new OrgUnit{ Code = "70093", CurrentName = CreateOrgName("ระยอง","Rayong")},
                    new OrgUnit{ Code = "70094", CurrentName = CreateOrgName("ตราด","Trad")},
                    new OrgUnit{ Code = "70101", CurrentName = CreateOrgName("หนองคาย","Nong Khai")},
                    new OrgUnit{ Code = "70102", CurrentName = CreateOrgName("เลย","Loei")},
                    new OrgUnit{ Code = "70103", CurrentName = CreateOrgName("อุดรธานี","Udon Thani")},
                    new OrgUnit{ Code = "70104", CurrentName = CreateOrgName("หนองบัวลำภู","Nong Bua Lamphu")},
                    new OrgUnit{ Code = "70111", CurrentName = CreateOrgName("นครพนม","Nakhon Phanom")},
                    new OrgUnit{ Code = "70112", CurrentName = CreateOrgName("มุกดาหาร","Mukdahan")},
                    new OrgUnit{ Code = "70113", CurrentName = CreateOrgName("สกลนคร","Sakon Nakhon")},
                    new OrgUnit{ Code = "70180", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคเหนือตอนล่าง 2","The Lower northern province 2")},
                    new OrgUnit{ Code = "70170", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคเหนือตอนล่าง 1","The Lower northern province 1")},
                    new OrgUnit{ Code = "70160", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคเหนือตอนบน 2","The upper northern provinces 2")},
                    new OrgUnit{ Code = "70150", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคเหนือตอนบน 1","The upper northern provinces 1")},
                    new OrgUnit{ Code = "70140", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคตะวันออกเฉียงเหนือตอนล่าง 2","Lower North of East provinces 2")},
                    new OrgUnit{ Code = "70130", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคตะวันออกเฉียงเหนือตอนล่าง 1","Lower North of East provinces 1")},
                    new OrgUnit{ Code = "70120", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคตะวันออกเฉียงเหนือตอนกลาง","North Central provinces.")},
                    new OrgUnit{ Code = "70080", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคใต้ชายแดน","Southern Border Provinces")},
                    new OrgUnit{ Code = "70070", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคใต้ฝั่งอันดามัน","The southern provinces along the Andaman coast")},
                    new OrgUnit{ Code = "70060", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคใต้ฝั่งอ่าวไทย","The southern coast of the Gulf of Thailand.")},
                    new OrgUnit{ Code = "70050", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคกลางตอนล่าง 2","Lower central provinces 2")},
                    new OrgUnit{ Code = "70040", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคกลางตอนล่าง 1","Lower central provinces 1")},
                    new OrgUnit{ Code = "70030", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคกลางตอนกลาง","The central province of Central")},
                    new OrgUnit{ Code = "70020", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคกลางตอนบน 2","The North Central Province 2")},
                    new OrgUnit{ Code = "70010", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคกลางตอนบน 1","The North Central Province 1")},
                    new OrgUnit{ Code = "70184", CurrentName = CreateOrgName("อุทัยธานี","Uthai Thani")},
                    new OrgUnit{ Code = "70183", CurrentName = CreateOrgName("นครสวรรค์","Nakhon Sawan")},
                    new OrgUnit{ Code = "70182", CurrentName = CreateOrgName("พิจิตร","Phichit")},
                    new OrgUnit{ Code = "70181", CurrentName = CreateOrgName("กำแพงเพชร","Kamphaeng Phet")},
                    new OrgUnit{ Code = "70175", CurrentName = CreateOrgName("อุตรดิตถ์","Uttaradit")},
                    new OrgUnit{ Code = "70174", CurrentName = CreateOrgName("เพชรบูรณ์","Phetchabun")},
                    new OrgUnit{ Code = "70173", CurrentName = CreateOrgName("สุโขทัย","Sukhothai")},
                    new OrgUnit{ Code = "70172", CurrentName = CreateOrgName("พิษณุโลก","Phitsanulok")},
                    new OrgUnit{ Code = "70171", CurrentName = CreateOrgName("ตาก","Tak")},
                    new OrgUnit{ Code = "70164", CurrentName = CreateOrgName("แพร่","Phrae")},
                    new OrgUnit{ Code = "70163", CurrentName = CreateOrgName("เชียงราย","Chiang Rai")},
                    new OrgUnit{ Code = "70162", CurrentName = CreateOrgName("พะเยา","Phayao")},
                    new OrgUnit{ Code = "70161", CurrentName = CreateOrgName("น่าน","Nan")},
                    new OrgUnit{ Code = "70154", CurrentName = CreateOrgName("ลำพูน","Lamphun")},
                    new OrgUnit{ Code = "70153", CurrentName = CreateOrgName("ลำปาง","Lampang")},
                    new OrgUnit{ Code = "70152", CurrentName = CreateOrgName("แม่ฮ่องสอน","Mae Hong Son")},
                    new OrgUnit{ Code = "70151", CurrentName = CreateOrgName("เชียงใหม่","Chiang Mai")},
                    new OrgUnit{ Code = "70144", CurrentName = CreateOrgName("ชัยภูมิ","Chaiyaphum")},
                    new OrgUnit{ Code = "70143", CurrentName = CreateOrgName("บุรีรัมย์","Buri Ram")},
                    new OrgUnit{ Code = "70142", CurrentName = CreateOrgName("นครราชสีมา","Nakhon Ratchasima")},
                    new OrgUnit{ Code = "70141", CurrentName = CreateOrgName("สุรินทร์","Surin")},
                    new OrgUnit{ Code = "70134", CurrentName = CreateOrgName("อุบลราชธานี","Ubon Ratchathani")},
                    new OrgUnit{ Code = "70133", CurrentName = CreateOrgName("ยโสธร","Yasothon")},
                    new OrgUnit{ Code = "70132", CurrentName = CreateOrgName("ศรีสะเกษ","Si Sa Ket")},
                    new OrgUnit{ Code = "70131", CurrentName = CreateOrgName("อำนาจเจริญ","Amnat Charoen")},
                    new OrgUnit{ Code = "70124", CurrentName = CreateOrgName("กาฬสินธุ์","Kalasin")},
                    new OrgUnit{ Code = "70123", CurrentName = CreateOrgName("มหาสารคาม","Maha Sarakham")},
                    new OrgUnit{ Code = "70122", CurrentName = CreateOrgName("ขอนแก่น","Khon Kaen")},
                    new OrgUnit{ Code = "70121", CurrentName = CreateOrgName("ร้อยเอ็ด","Roi Et")},
                    new OrgUnit{ Code = "70090", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคตะวันออก","The Eastern Province")},
                    new OrgUnit{ Code = "70100", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคตะวันออกเฉียงเหนือตอนบน ๑","The province of Upper Northeast ๑")},
                    new OrgUnit{ Code = "70110", CurrentName = CreateOrgName("กลุ่มจังหวัดภาคตะวันออกเฉียงเหนือตอนบน ๒","The province of Upper Northeast ๒")},
                    new OrgUnit{ Code = "70150", CurrentName = CreateOrgName("บึงกาฬ","Kan")},
                }
             );
        }

        private void Create80800()
        {
            PersistGovernmentUnit
            (
                "80800", CreateOrgName("กองทุนและเงินทุนหมุนเวียน", ""),
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "80801", CurrentName = CreateOrgName("เงินทุนหมุนเวียนผลิตป้ายจราจร","Working capital, production, traffic sign")},
                    new OrgUnit{ Code = "80802", CurrentName = CreateOrgName("เงินทุนหมุนเวียนเพื่อจัดทำแผ่นเลขทะเบียนรถ","Working capital to prepare the car number plate.")},
                    new OrgUnit{ Code = "80803", CurrentName = CreateOrgName("เงินทุนหมุนเวียนกรมการขนส่งทางอากาศ","Working capital Department of Civil Aviation")},
                    new OrgUnit{ Code = "80804", CurrentName = CreateOrgName("กองทุนเพื่อความปลอดภัยในการใช้รถใช้ถนน","Fund for the safety of the road.")},
                }
            );
        }
        private void PersistGovernmentUnit(string code, OrgName name, IList<OrgUnit> orgunits)
        {
            var govUnit = new Organization
            {
                Code = code,
                CurrentName = name,
                OrgUnits = orgunits
            };
            govUnit.Persist(this.context);
        }

        private OrgName CreateOrgName(string th, string en)
        {
            return new OrgName { Name = new MultilingualString("th-TH", th, "en-US", en) };
        }
    }

}
