using System.Xml.Serialization;
namespace Xml2CSharp
{
    [XmlRoot(ElementName = "Answer1")]
    public class Answer1
    {
        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "Correct")]
        public string Correct { get; set; }
    }

    [XmlRoot(ElementName = "Answer2")]
    public class Answer2
    {
        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "Correct")]
        public string Correct { get; set; }
    }

    [XmlRoot(ElementName = "Answer3")]
    public class Answer3
    {
        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "Correct")]
        public string Correct { get; set; }
    }

    [XmlRoot(ElementName = "Answer4")]
    public class Answer4
    {
        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "Correct")]
        public string Correct { get; set; }
    }

    [XmlRoot(ElementName = "Question")]
    public class Question
    {
        [XmlElement(ElementName = "Answer1")]
        public Answer1 Answer1 { get; set; }
        [XmlElement(ElementName = "Answer2")]
        public Answer2 Answer2 { get; set; }
        [XmlElement(ElementName = "Answer3")]
        public Answer3 Answer3 { get; set; }
        [XmlElement(ElementName = "Answer4")]
        public Answer4 Answer4 { get; set; }
        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Questions")]
    public class Questions
    {
        [XmlElement(ElementName = "Question")]
        public Question Question { get; set; }
    }

}

