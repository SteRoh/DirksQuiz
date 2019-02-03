   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace XmlAnswer
{
	[XmlRoot(ElementName="Answer")]
	public class Answer {
		[XmlAttribute(AttributeName="Text")]
		public string Text { get; set; }
		[XmlAttribute(AttributeName="State")]
		public string State { get; set; }
	}

	[XmlRoot(ElementName="Answers")]
	public class Answers {
		[XmlElement(ElementName="Answer")]
		public List<Answer> Answer { get; set; }
	}

}
