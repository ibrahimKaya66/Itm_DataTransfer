using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class GroupCodeMethod : IGroupCodeMethod
    {
        private readonly IGroupCodeService groupCodeService;
        private readonly IFactoryService factoryService;
        private readonly IMapper mapper;

        public GroupCodeMethod(IGroupCodeService groupCodeService, IFactoryService factoryService, IMapper mapper)
        {
            this.groupCodeService = groupCodeService;
            this.factoryService = factoryService;
            this.mapper = mapper;
        }
        public List<GroupCodeDTO>? Get()
        {
            try
            {
                var models = groupCodeService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responseDtos = new List<GroupCodeDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<GroupCodeDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<GroupCodeDTO?> Get(int id)
        {
            var model = await groupCodeService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<GroupCodeDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<GroupCodeDTO?> Post(GroupCodeDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<GroupCode>(model); // DTO'yu GroupCode'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await groupCodeService.AddAsync(entity);
                var responseDto = mapper.Map<GroupCodeDTO>(entity); // GroupCode'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<GroupCodeDTO?> Put(int id, GroupCodeDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await groupCodeService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            mapper.Map(model, entity); // GroupCodeDTO nesnesini entity'ye dönüştür
            entity.ModifiedBy = "apiUser";
            entity.ModifiedDate = now;
            try
            {
                await groupCodeService.UpdateAsync(entity);
                var responseDto = mapper.Map<GroupCodeDTO>(entity); // Güncellenmiş entity'yi GroupCodeDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<GroupCodeDTO?> Delete(int id)
        {
            var model = await groupCodeService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await groupCodeService.RemoveAsync(model);
                    var responseDto = mapper.Map<GroupCodeDTO>(model);
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
