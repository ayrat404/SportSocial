using System;
using System.ComponentModel.DataAnnotations;
using DAL.DomainModel.EnumProperties;
using Knoema.Localization;

namespace BLL.Login.ViewModels
{
    [Localized]
    public class RegistratioinModel
    {
        [Required(ErrorMessage = "�� ������ ����� ��������")]
        [RegularExpression(@"^[0-9]{11,13}$", ErrorMessage = "����� �������� ������ ��������� ������ ����� � ������� <��� ������><�����> ��� ������ \"+\".")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "�� ������� ���")]
        public string Name { get; set; }
    }
}