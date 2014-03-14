using System;
using NoNameLib.Debug;
using NoNameLib.Enums;
using NoNameLib.Extension;

namespace NoNameLib
{
    [Serializable]
    public class NoNameLibException : Exception
    {
        private Enum errorEnumValue;
        private readonly string additionalInformation = string.Empty;

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>		
        /// <param name="errorEnumValue">Enum value of the error</param>		
        public NoNameLibException(Enum errorEnumValue)
            : base(errorEnumValue.ToString())
        {
            this.errorEnumValue = errorEnumValue;
        }

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>		
        /// <param name="errorEnumValue">Enum value of the error</param>		
        /// <param name="ex">Exception that was thrown and is represented by this Obymobi exception</param>		
        public NoNameLibException(Enum errorEnumValue, Exception ex)
            : base(errorEnumValue.ToString())
        {
            this.errorEnumValue = errorEnumValue;
            this.additionalInformation += string.Format("InnerException: {0}\r\n\r\n Inner Stack Trace: {1}", ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>
        /// <param name="errorEnumValue">Enum value of the error</param>		
        /// <param name="message">string.Format style message</param>		
        /// <param name="args">Arguments to be used in the message parameter</param>		
        public NoNameLibException(Enum errorEnumValue, string message, params object[] args)
            : base(message.FormatSafe(args))
        {
            this.errorEnumValue = errorEnumValue;
            this.additionalInformation = message.FormatSafe(args);
        }

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>
        /// <param name="errorEnumValue">Enum value of the error</param>		
        /// <param name="ex">Exception that was thrown and is represented by this Obymobi exception</param>		
        /// <param name="message">string.Format style message</param>			
        /// <param name="args">Arguments to be used in the message parameter</param>		
        public NoNameLibException(Enum errorEnumValue, Exception ex, string message, params object[] args)
            : base(message.FormatSafe(args))
        {
            this.errorEnumValue = errorEnumValue;
            this.additionalInformation = message.FormatSafe(args);

			if (ex != null)
				this.additionalInformation += "\r\n\r\nInnerException: {0}\r\n\r\n Inner Stack Trace: {1}".FormatSafe(ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// Gets or sets the ErrorEnumValue
        /// </summary>
        public Enum ErrorEnumValue
        {
            get
            {
                if (this.errorEnumValue == null)
                    return UnspecifiedError.Unknown;
                
                return this.errorEnumValue;
            }
            protected set { this.errorEnumValue = value; }
        }

        public string ErrorEnumType
        {
            get
            {
                if (this.errorEnumValue == null)
                    return "null";
                
                return this.errorEnumValue.GetType().ToString();
            }
        }

        public string ErrorEnumValueText
        {
            get
            {
                if (this.errorEnumValue == null)
                    return "null";
                
                return this.errorEnumValue.ToString();
            }
        }

        public int ErrorEnumValueInt
        {
            get
            {
                if (this.errorEnumValue == null)
                    return -1;

                try
                {
                    return this.errorEnumValue.ToInt();
                }
                catch
                {
                    return -1;
                }
            }
        }

        public string BaseMessage
        {
            get { return base.Message; }
        }

        private bool writtenToDebug;
        /// <summary>
        /// Implementation of Message containing all info specific for obymobi
        /// </summary>        
        public override string Message
        {
            get
            {
                string text = "Message: {0}\r\n\r\nError Type: {1}\r\n\r\nErrorValue: {2} ({3})\r\n\r\nAdditional Information: {4}\r\n\r\n".FormatSafe(base.Message, this.ErrorEnumType, this.ErrorEnumValueText, this.ErrorEnumValueInt, this.additionalInformation);

                if (!this.writtenToDebug && TestUtil.IsPcDeveloper)
                {
                    System.Diagnostics.Debug.WriteLine(text);
                    this.writtenToDebug = true;
                }

                return text;
            }
        }
    }
}
