using AutoMapper;
using EZNEW.Domain.Sys.Model;
using EZNEW.DTO.Sys;
using EZNEW.Entity.Sys;
using EZNEW.Mapper.Convention;
using EZNEW.ValueType;
using EZNEW.ViewModel.Sys;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// 对象映射配置
    /// </summary>
    public static class ObjectMapConfig
    {
        public static void Configure()
        {
            ConventionMapper.ConfigureMap(cfg =>
            {
                var defaultMemberValidation = MemberList.None;

                #region Role

                cfg.CreateMap<RoleEntity, Role>(defaultMemberValidation)
                    .ForMember(r => r.Parent, r => r.MapFrom<RoleParentResolver>());
                cfg.CreateMap<Role, RoleEntity>(defaultMemberValidation)
                    .ForMember(re => re.Parent, r => r.MapFrom(rs => rs.Parent.Id));

                #endregion

                #region Permission Group

                cfg.CreateMap<PermissionGroup, PermissionGroupEntity>(defaultMemberValidation)
                    .ForMember(r => r.Parent, re => re.MapFrom(rs => rs.Parent.Id));
                cfg.CreateMap<PermissionGroupEntity, PermissionGroup>(defaultMemberValidation)
                    .ForMember(re => re.Parent, r => r.MapFrom<PermissionGroupParentResolver>());

                #endregion

                #region Permission

                cfg.CreateMap<Permission, PermissionEntity>(defaultMemberValidation)
                    .ForMember(c => c.Group, re => re.MapFrom(rs => rs.Group.Id));
                cfg.CreateMap<PermissionEntity, Permission>(defaultMemberValidation)
                    .ForMember(c => c.Group, re => re.MapFrom<PermissionGroupResolver>());

                #endregion

                #region Operation Group

                cfg.CreateMap<OperationGroup, OperationGroupEntity>(defaultMemberValidation)
                    .ForMember(r => r.Parent, re => re.MapFrom(rs => rs.Parent.Id));
                cfg.CreateMap<OperationGroupEntity, OperationGroup>(defaultMemberValidation)
                    .ForMember(re => re.Parent, r => r.MapFrom<OperationGroupParentResolver>());

                #endregion

                #region Operation

                cfg.CreateMap<Operation, OperationEntity>(defaultMemberValidation)
                    .ForMember(c => c.Group, re => re.MapFrom(rs => rs.Group.Id));
                cfg.CreateMap<OperationEntity, Operation>(defaultMemberValidation)
                    .ForMember(c => c.Group, re => re.MapFrom<OperationGroupResolver>());

                #endregion
            });
        }
    }

    #region Role Parent Resolver

    public class RoleParentResolver : IValueResolver<RoleEntity, Role, Role>
    {
        public Role Resolve(RoleEntity source, Role destination, Role destMember, ResolutionContext context)
        {
            return Role.Create(source?.Parent ?? 0);
        }
    }

    #endregion

    #region Permission Group Parent Resolver

    public class PermissionGroupParentResolver : IValueResolver<PermissionGroupEntity, PermissionGroup, PermissionGroup>
    {
        public PermissionGroup Resolve(PermissionGroupEntity source, PermissionGroup destination, PermissionGroup destMember, ResolutionContext context)
        {
            return PermissionGroup.Create(source?.Parent ?? 0);
        }
    }

    #endregion

    #region Permission Group Resolver

    public class PermissionGroupResolver : IValueResolver<PermissionEntity, Permission, PermissionGroup>
    {
        public PermissionGroup Resolve(PermissionEntity source, Permission destination, PermissionGroup destMember, ResolutionContext context)
        {
            return PermissionGroup.Create(source?.Group ?? 0);
        }
    }

    #endregion

    #region Operation Group Parent Resolver

    public class OperationGroupParentResolver : IValueResolver<OperationGroupEntity, OperationGroup, OperationGroup>
    {
        public OperationGroup Resolve(OperationGroupEntity source, OperationGroup destination, OperationGroup destMember, ResolutionContext context)
        {
            return OperationGroup.Create(source?.Parent ?? 0);
        }
    }

    #endregion

    #region Operation Group Resolver

    public class OperationGroupResolver : IValueResolver<OperationEntity, Operation, OperationGroup>
    {
        public OperationGroup Resolve(OperationEntity source, Operation destination, OperationGroup destMember, ResolutionContext context)
        {
            return OperationGroup.Create(source?.Group ?? 0);
        }
    }

    #endregion
}
