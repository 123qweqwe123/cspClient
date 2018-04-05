using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace cspClient.utils
{
    class CardReader
    {
        [DllImport("termb.dll")]
        public static extern short CVR_InitComm(int port);
        [DllImport("termb.dll")]
        public static extern short CVR_CloseComm();
        [DllImport("termb.dll")]
        public static extern short CVR_Authenticate();
        [DllImport("termb.dll")]
        public static extern short CVR_Read_Content(int Active);
        [DllImport("termb.dll")]
        public static extern short GetPeopleName(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetPeopleAddress(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetPeopleIDCode(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetPeopleSex(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetPeopleBirthday(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetPeopleNation(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetDepartment(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetStartDate(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short GetEndDate(StringBuilder lpReturnedString, ref short nReturnLen);
        [DllImport("termb.dll")]
        public static extern short CVR_ReadBaseMsg(StringBuilder pucCHMsgb, short puiCHMsgLen, StringBuilder pucPHMsg, short puiPHMsgLen, int mode);

    }
}
