using System.Collections.Generic;

namespace HY.Modeles.DTO
{
    /// <summary>
    /// 传输对象，只包含ID和Name
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Key-Value键值对
    /// </summary>
    public class KeyValueDTO 
    { 
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }
    
    }

    /// <summary>
    /// Key-Value键值对
    /// </summary>
    public class KeyValueExDTO
    {
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        /// Value
        /// </summary>
        public List<KeyValueDTO> Value { get; set; } = new List<KeyValueDTO>();

    }

}
