using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class LineMethod : ILineMethod
    {
        private readonly ILineService lineService;
        private readonly IFactoryService factoryService;
        private readonly IMapper mapper;

        public LineMethod(ILineService lineService, IFactoryService factoryService, IMapper mapper)
        {
            this.lineService = lineService;
            this.factoryService = factoryService;
            this.mapper = mapper;
        }
        public List<LineDTO>? Get()
        {
            try
            {
                var models = lineService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responseDtos = new List<LineDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<LineDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<LineDTO?> Get(int id)
        {
            var model = await lineService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<LineDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<LineDTO?> Post(LineDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<Line>(model); // DTO'yu Line'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await lineService.AddAsync(entity);
                var responseDto = mapper.Map<LineDTO>(entity); // Line'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<LineDTO?> Put(int id, LineDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await lineService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            mapper.Map(model, entity); // LineDTO nesnesini entity'ye dönüştür
            entity.ModifiedBy = "apiUser";
            entity.ModifiedDate = now;
            try
            {
                await lineService.UpdateAsync(entity);
                var responseDto = mapper.Map<LineDTO>(entity); // Güncellenmiş entity'yi LineDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<LineDTO?> Delete(int id)
        {
            var model = await lineService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await lineService.RemoveAsync(model);
                    var responseDto = mapper.Map<LineDTO>(model);
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
