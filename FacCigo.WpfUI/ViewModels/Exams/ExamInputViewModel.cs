using FacCigo.Categories;
using FacCigo.Commands;
using FacCigo.Commands.Exams;
using FacCigo.Settings;
using Microsoft.Data.Sqlite;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using Volo.Abp.Validation;

namespace FacCigo.ViewModels.Exams
{
    public class ExamInputViewModel : ValidatableBindableBase, IExamInputViewModel,ITransientDependency
    {
        private Guid _id;
        private string _name;
        private CategoryDto _category;
        private ObservableCollection<CategoryDto> _categories;
        private decimal _price=0.00m;
        private string _referenceNo;
        private Guid _categoryId;
        private string _currency;
        private string _errorText;
        private readonly ISettingProvider SettingProvider;
        private readonly IEventAggregator EventAggregator;
        private readonly ICategoryAppService CategoryService;
        private readonly IExamAppService AppService;
        public bool NewExam { get; private set; } = true;
        private ExamDto updated;
        public ExamInputViewModel(ICategoryAppService categoryAppService,IExamAppService examAppService, ISettingProvider setting, IEventAggregator ea)
        {
            SettingProvider = setting;
            EventAggregator = ea;
            CategoryService = categoryAppService;
            AppService = examAppService;
            CreateCommand = new DelegateCommand<ICloseable>(Create);
            Categories = new ObservableCollection<CategoryDto>();
            Categories.AddRange(CategoryService.GetListAsync(new PagedAndSortedResultRequestDto()).Result.Items);
            Currency = SettingProvider.GetOrNullAsync(FacCigoSettings.PivotCurrency).Result;
        }
        public DelegateCommand<ICloseable> CreateCommand { get; private set; }
        public string Currency { get { return _currency; }
                              set { SetProperty(ref _currency, value); }
        }
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        [StringLength(ExamConsts.MaxLengthName, ErrorMessage = "La Taille ne correspond pas")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Vous devez fournir le Libelle")]
        public string  Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez selectionner la categorie")]
        public CategoryDto Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value);
                if (NewExam) ReferenceNo = value.ShortName+"-"+LastReference(value.Id);
                else
                {
                    if (updated != null && updated.CategoryId != value.Id)
                    {
                        ReferenceNo = value.ShortName + "-" + LastReference(value.Id);
                    }
                    else ReferenceNo = updated.ReferenceNo;


                }
            }
        }
        public ObservableCollection<CategoryDto> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez fournir le prix")]
        public decimal Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        public string ReferenceNo
        {
            get { return _referenceNo; }
            set { SetProperty(ref _referenceNo, value); }
        }
        public Guid CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value);
               
                 if (Category == null || Category.Id != value) Category = Categories.FirstOrDefault(c => c.Id == value);
                
               
                
            }
        }
        public string ErrorText {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }
        public async void Create(ICloseable window)
        {
            try
            {
                if (HasErrors) return;
                ExamInput input = new ExamInput() { CategoryId = Category.Id, Name = Name, Price = Price,CurrencyId=Currency,ReferenceNo=ReferenceNo};
                ExamDto examDto = await (NewExam ?AppService.CreateAsync(input): AppService.UpdateAsync(Id,input));
                if (NewExam) EventAggregator.GetEvent<ExamAddedEvent>().Publish(examDto);
                else EventAggregator.GetEvent<ExamUpdatedEvent>().Publish(examDto);
                window.Close();
            }
            catch (SqliteException sqlEx)
            {
                ErrorText = sqlEx.Message;
            }
            catch (BusinessException busEx)
            {
                ErrorText = busEx.Message;
            }



        }
        public bool IsValid { get { return !HasErrors; } }
        private string LastReference(Guid id)
        {
            //return await AppService.Get(new ExamGetListInput() { CategoryId = Category.Id }));
            return AppService.NextReferenceNo(id).Result;

        }

        public void UpdateModel(ExamDto dto)
        {
            updated = dto;
            NewExam = false;
            Id = dto.Id;
            Name = dto.Name;
            Price = dto.Price;
            ReferenceNo = dto.ReferenceNo;
            CategoryId = dto.CategoryId;
            Currency = dto.CurrencyId;
        }
    }
}
