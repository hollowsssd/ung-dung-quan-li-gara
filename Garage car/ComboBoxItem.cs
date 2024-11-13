using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_car
{
    public class ComboBoxItem
    {
        public string Ma { get; set; }
        public string Ten { get; set; }

        // Trả về chuỗi kết hợp để hiển thị trong ComboBox
        public override string ToString()
        {
            return $"{Ma} | {Ten}";
        }
    }

}
