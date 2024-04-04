using AutoMapper;
using ItmProject.Business.Methods.Abstract;
using ItmProject.Business.Services.Abstract;
using ItmProject.Model.DTOs;
using ItmProject.Model.Models.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class GroupMethod : IGroupMethod
    {
        private readonly IGroupService groupService;
        private readonly IFactoryService factoryService;
        private readonly IMapper mapper;

        public GroupMethod(IGroupService groupService, IFactoryService factoryService, IMapper mapper)
        {
            this.groupService = groupService;
            this.factoryService = factoryService;
            this.mapper = mapper;
        }
        public List<GroupDTO>? Get()
        {
            try
            {
                var models = groupService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responseDtos = new List<GroupDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<GroupDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<GroupDTO?> Get(int id)
        {
            var model = await groupService.GetAsync(id);
            if (model != null)
            {
                var responseDto = mapper.Map<GroupDTO>(model);
                return responseDto;
            }

            else
                return null;
        }

        public async Task<GroupDTO?> Post(GroupDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<Group>(model); // DTO'yu Group'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await groupService.AddAsync(entity);
                var responseDto = mapper.Map<GroupDTO>(entity); // Group'ı DTO'ya dönüştür
                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<GroupDTO?> Put(int id, GroupDTO model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await groupService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            mapper.Map(model, entity); // GroupDTO nesnesini entity'ye dönüştür
            entity.ModifiedBy = "apiUser";
            entity.ModifiedDate = now;
            try
            {
                await groupService.UpdateAsync(entity);
                var responseDto = mapper.Map<GroupDTO>(entity); // Güncellenmiş entity'yi GroupDTO'ya dönüştür

                return responseDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<GroupDTO?> Delete(int id)
        {
            var model = await groupService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await groupService.RemoveAsync(model);
                    var responseDto = mapper.Map<GroupDTO>(model);
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
        public List<GroupDTO>? GetWithGroupCode(int? groupCodeId)
        {
            try
            {
                var models = groupService.GetAll().Where(m => m.IsDeleted == false && m.GroupCodeId == groupCodeId).ToList();
                var responseDtos = new List<GroupDTO>();
                foreach (var model in models)
                {
                    var responseDto = mapper.Map<GroupDTO>(model);
                    responseDtos.Add(responseDto);
                }

                return responseDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
