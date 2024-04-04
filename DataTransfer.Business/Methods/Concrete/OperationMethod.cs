using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class OperationMethod : IOperationMethod
    {
        private readonly IOperationService operationService;
        private readonly IFactoryService factoryService;
        private readonly IMapper mapper;

        public OperationMethod(IOperationService operationService, IFactoryService factoryService, IMapper mapper)
        {
            this.operationService = operationService;
            this.factoryService = factoryService;
            this.mapper = mapper;
        }
        public List<OperationDTO>? Get()
        {
            try
            {
                var models = operationService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responseDtos = new List<OperationDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<OperationDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<OperationDTO?> Get(int id)
        {
            var model = await operationService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<OperationDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<OperationDTO?> Post(OperationDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<Operation>(model); // DTO'yu Operation'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await operationService.AddAsync(entity);
                var responseDto = mapper.Map<OperationDTO>(entity); // Operation'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<OperationDTO?> Put(int id, OperationDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await operationService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            //mapper.Map(model, entity); // OperationDTO nesnesini entity'ye dönüştür
            try
            {
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Code = model.Code;
                entity.TypeId = model.TypeId;
                entity.ReferanceCode = model.ReferanceCode;
                entity.MachineId = model.MachineId;
                entity.OperationGroupId = model.OperationGroupId;
                entity.DepartmentId = model.DepartmentId;
                entity.ApparatId = model.ApparatId;
                entity.TimeSecond = model.TimeSecond;
                entity.Tolerance = model.Tolerance;

                entity.ModifiedBy = "apiUser";
                entity.ModifiedDate = now;
                await operationService.UpdateAsync(entity);
                var responseDto = mapper.Map<OperationDTO>(entity); // Güncellenmiş entity'yi OperationDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<OperationDTO?> Delete(int id)
        {
            var model = await operationService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await operationService.RemoveAsync(model);
                    var responseDto = mapper.Map<OperationDTO>(model);
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
