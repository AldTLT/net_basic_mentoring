using System.Xml.Serialization;

namespace CatalogLib.Enums
{
    public enum Genre
    {
        [XmlEnum]
        Computer,
        [XmlEnum]
        Fantasy,
        [XmlEnum]
        Romance,
        [XmlEnum]
        Horror,
        [XmlEnum("Science Fiction")]
        ScienceFiction
    }
}
