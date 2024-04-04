using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class DepartmentMethod : IDepartmentMethod
    {
        private readonly IDepartmentService departmentService;
        private readonly IFactoryService factoryService;
        private readonly IMapper mapper;

        public DepartmentMethod(IDepartmentService departmentService, IFactoryService factoryService, IMapper mapper)
        {
            this.departmentService = departmentService;
            this.factoryService = factoryService;
            this.mapper = mapper;
        }
        public List<DepartmentDTO>? Get()
        {
            try
            {
                var models = departmentService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responseDtos = new List<DepartmentDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<DepartmentDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DepartmentDTO?> Get(int id)
        {
            var model = await departmentService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<DepartmentDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<DepartmentDTO?> Post(DepartmentDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<Department>(model); // DTO'yu Department'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await departmentService.AddAsync(entity);
                var responseDto = mapper.Map<DepartmentDTO>(entity); // Department'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DepartmentDTO?> Put(int id, DepartmentDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await departmentService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            mapper.Map(model, entity); // DepartmentDTO nesnesini entity'ye dönüştür
            entity.ModifiedBy = "apiUser";
            entity.ModifiedDate = now;
            try
            {
                await departmentService.UpdateAsync(entity);
                var responseDto = mapper.Map<DepartmentDTO>(entity); // Güncellenmiş entity'yi DepartmentDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DepartmentDTO?> Delete(int id)
        {
            var model = await departmentService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await departmentService.RemoveAsync(model);
                    var responseDto = mapper.Map<DepartmentDTO>(model);
                    return responseDto;
                }
                catch (Exception)
                {
                    return null;
                }

            }
            else
                return null;
        }
    }
}
