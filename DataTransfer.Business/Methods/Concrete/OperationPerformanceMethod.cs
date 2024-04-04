using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class OperationPerformanceMethod : IOperationPerformanceMethod
    {
        private readonly IOperationPerformanceService operationPerformanceService;
        private readonly IFactoryService factoryService;
        private readonly IMapper mapper;

        public OperationPerformanceMethod(IOperationPerformanceService operationPerformanceService, IFactoryService factoryService, IMapper mapper)
        {
            this.operationPerformanceService = operationPerformanceService;
            this.factoryService = factoryService;
            this.mapper = mapper;
        }
        public List<OperationPerformanceDTO>? Get()
        {
            try
            {
                var models = operationPerformanceService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responseDtos = new List<OperationPerformanceDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<OperationPerformanceDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<OperationPerformanceDTO?> Get(int id)
        {
            var model = await operationPerformanceService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<OperationPerformanceDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<OperationPerformanceDTO?> Post(OperationPerformanceDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<OperationPerformance>(model); // DTO'yu OperationPerformance'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await operationPerformanceService.AddAsync(entity);
                var responseDto = mapper.Map<OperationPerformanceDTO>(entity); // OperationPerformance'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<OperationPerformanceDTO?> Put(int id, OperationPerformanceDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await operationPerformanceService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            //mapper.Map(model, entity); // OperationPerformanceDTO nesnesini entity'ye dönüştür
            try
            {
                entity.Date_ = model.Date_;
                entity.OperationId = model.OperationId;
                entity.OperatorId = model.OperatorId;
                entity.Performance = model.Performance;
                entity.LineId = model.LineId;

                entity.ModifiedBy = "apiUser";
                entity.ModifiedDate = now;
                await operationPerformanceService.UpdateAsync(entity);
                var responseDto = mapper.Map<OperationPerformanceDTO>(entity); // Güncellenmiş entity'yi OperationPerformanceDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<OperationPerformanceDTO?> Delete(int id)
        {
            var model = await operationPerformanceService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await operationPerformanceService.RemoveAsync(model);
                    var responseDto = mapper.Map<OperationPerformanceDTO>(model);
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
