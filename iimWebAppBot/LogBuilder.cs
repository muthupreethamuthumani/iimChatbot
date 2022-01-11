using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace iimWebAppBot
{
    public class LogBuilder
    {
        
            /*Created By   :   Muthupreetha Muthumani
            Created Time :   10-01-2022
            Updated By   :   Muthupreetha Muthumani
            Updated Time :   10-01-2022
            * Getting errors from exception 
            * Getting frameCount from GetFrame() object
            * Forming error by getting Error Message ,File Path,Class Name, Method Name,Line Number,Column Number and stored it in variable 
              strErrorFormation.
            * Return strErrorFormation.

           */

            public string ErrorsFormation(Exception ex, [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {

                var stacktrace = new StackTrace(ex, true);

                StackFrame stack = stacktrace.GetFrame(stacktrace.FrameCount - 1);



                string errorformation = "Message: " + ex.Message + " || FilePath: " + filePath + " || ClassName: " + stack.GetMethod().DeclaringType + " || MethodName: " + methodName + " || LineNumber: " + lineNumber;

                return errorformation;


            }


        /* Created By   : Muthupreetha Muthumani
         Created Time :   10-01-2022
         Updated By   :   Muthupreetha Muthumani
         Updated Time :   10-01-2022
         * returns method name of a method which call this method.

        */
        public string MethodName([CallerMemberName] string methodName = "")
            {
                return methodName;
            }

        
    }
}
