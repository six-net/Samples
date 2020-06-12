using AutoMapper;
using EZNEW.Module.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Entity.Sys;
using EZNEW.ValueType;
using EZNEW.ViewModel.Sys.Filter;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.ViewModel.Sys.Response;
using System;
using System.Linq;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// Object map config
    /// </summary>
    public static class ObjectMapConfig
    {
        /// <summary>
        /// 配置对象映射转换
        /// </summary>
        /// <param name="cfg"></param>
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            if (cfg == null)
            {
                return;
            }

            var defaultMemberValidation = MemberList.None;

            #region User

            cfg.CreateMap<UserEntity, User>(defaultMemberValidation)
                .ForMember(u => u.Contact, u => u.MapFrom<EntityContactResolver>());
            cfg.CreateMap<User, UserEntity>(defaultMemberValidation)
                .ForMember(u => u.Email, u => u.MapFrom(ue => ue.Contact.Email))
                .ForMember(u => u.Mobile, u => u.MapFrom(ue => ue.Contact.Mobile))
                .ForMember(u => u.QQ, u => u.MapFrom(ue => ue.Contact.QQ));

            cfg.CreateMap<User, UserDto>(defaultMemberValidation)
                .ConvertUsing<UserToUserDtoConverter>();
            cfg.CreateMap<UserCmdDto, User>(defaultMemberValidation)
                .ConvertUsing<UserCmdDtoToUserConverter>();
            cfg.CreateMap<UserDto, UserViewModel>(defaultMemberValidation)
                .ConvertUsing<UserDtoToUserViewModelConverter>();
            cfg.CreateMap<UserDto, EditUserViewModel>(defaultMemberValidation)
                .ConvertUsing<UserDtoToEditUserViewModelConverter>();
            cfg.CreateMap<UserViewModel, UserCmdDto>(defaultMemberValidation)
                .ConvertUsing<UserViewModelToUserCmdDtoConverter>();
            cfg.CreateMap<UserFilterViewModel, UserFilterDto>(defaultMemberValidation);
            cfg.CreateMap<ModifyPasswordViewModel, ModifyPasswordCmdDto>(defaultMemberValidation);
            cfg.CreateMap<ModifyPasswordCmdDto, ModifyUserPassword>(defaultMemberValidation);

            //admin user
            cfg.CreateMap<User, AdminUser>(defaultMemberValidation);
            cfg.CreateMap<AdminUser, AdminUserDto>(defaultMemberValidation);
            cfg.CreateMap<AdminUserDto, AdminUserViewModel>(defaultMemberValidation)
                .ForMember(u => u.Email, u => u.MapFrom(ue => ue.Contact.Email))
                .ForMember(u => u.Mobile, u => u.MapFrom(ue => ue.Contact.Mobile))
                .ForMember(u => u.QQ, u => u.MapFrom(ue => ue.Contact.QQ));
            cfg.CreateMap<AdminUserDto, EditUserViewModel>(defaultMemberValidation)
                .ForMember(u => u.Email, u => u.MapFrom(ue => ue.Contact.Email))
                .ForMember(u => u.Mobile, u => u.MapFrom(ue => ue.Contact.Mobile))
                .ForMember(u => u.QQ, u => u.MapFrom(ue => ue.Contact.QQ));
            cfg.CreateMap<EditUserViewModel, AdminUserCmdDto>(defaultMemberValidation)
                .ForMember(u => u.Contact, u => u.MapFrom<DtoContactResolver>());
            cfg.CreateMap<AdminUserCmdDto, AdminUser>(defaultMemberValidation)
                .ForMember(d => d.CreateDate, s => s.Ignore())
                .ForMember(d => d.LastLoginDate, s => s.Ignore());
            cfg.CreateMap<UserViewModel, AdminUserViewModel>(defaultMemberValidation);
            cfg.CreateMap<AdminUserFilterViewModel, AdminUserFilterDto>(defaultMemberValidation);

            #endregion

            #region Role

            cfg.CreateMap<RoleEntity, Role>(defaultMemberValidation)
                .ForMember(r => r.Parent, r => r.MapFrom<RoleParentResolver>());
            cfg.CreateMap<Role, RoleEntity>(defaultMemberValidation)
                .ForMember(re => re.Parent, r => r.MapFrom(rs => rs.Parent.SysNo));
            cfg.CreateMap<Role, RoleDto>(defaultMemberValidation);
            cfg.CreateMap<RoleDto, Role>(defaultMemberValidation);
            cfg.CreateMap<RoleCmdDto, Role>(defaultMemberValidation);
            cfg.CreateMap<EditRoleViewModel, RoleCmdDto>(defaultMemberValidation);
            cfg.CreateMap<RoleViewModel, RoleCmdDto>(defaultMemberValidation);
            cfg.CreateMap<RoleDto, RoleViewModel>(defaultMemberValidation);
            cfg.CreateMap<RoleDto, EditRoleViewModel>(defaultMemberValidation);
            cfg.CreateMap<RoleFilterViewModel, RoleFilterDto>(defaultMemberValidation);
            cfg.CreateMap<ModifyRoleAuthorizeCmdDto, ModifyRoleAuthorize>(defaultMemberValidation)
                .ForMember(c => c.Binds, ce => ce.MapFrom(cs => cs.Binds.Select(cm => new Tuple<Role, Authority>(cm.Item1.MapTo<Role>(), cm.Item2.MapTo<Authority>()))))
                .ForMember(c => c.UnBinds, ce => ce.MapFrom(cs => cs.UnBinds.Select(cm => new Tuple<Role, Authority>(cm.Item1.MapTo<Role>(), cm.Item2.MapTo<Authority>()))));

            #endregion

            #region AuthorityGroup

            cfg.CreateMap<AuthorityGroup, AuthorityGroupEntity>(defaultMemberValidation)
                .ForMember(r => r.Parent, re => re.MapFrom(rs => rs.Parent.SysNo));
            cfg.CreateMap<AuthorityGroupEntity, AuthorityGroup>(defaultMemberValidation)
                .ForMember(re => re.Parent, r => r.MapFrom<AuthorityGroupParentResolver>());
            cfg.CreateMap<AuthorityGroup, AuthorityGroupDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityGroupCmdDto, AuthorityGroup>(defaultMemberValidation);
            cfg.CreateMap<AuthorityGroupDto, AuthorityGroupViewModel>(defaultMemberValidation);
            cfg.CreateMap<AuthorityGroupDto, EditAuthorityGroupViewModel>(defaultMemberValidation);
            cfg.CreateMap<EditAuthorityGroupViewModel, AuthorityGroupCmdDto>(defaultMemberValidation);
            cfg.CreateMap<EditAuthorityGroupViewModel, SaveAuthorityGroupCmdDto>(defaultMemberValidation)
                .ForMember(a => a.AuthorityGroup, a => a.MapFrom(c => c));

            #endregion

            #region Authority

            cfg.CreateMap<Authority, AuthorityEntity>(defaultMemberValidation)
                .ForMember(c => c.Group, re => re.MapFrom(rs => rs.Group.SysNo));
            cfg.CreateMap<AuthorityEntity, Authority>(defaultMemberValidation)
                .ForMember(c => c.Group, re => re.MapFrom<AuthorityGroupResolver>());
            cfg.CreateMap<Authority, AuthorityDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityCmdDto, Authority>(defaultMemberValidation);
            cfg.CreateMap<AuthorityDto, AuthorityViewModel>(defaultMemberValidation);
            cfg.CreateMap<AuthorityDto, EditAuthorityViewModel>(defaultMemberValidation);
            cfg.CreateMap<EditAuthorityViewModel, AuthorityCmdDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityFilterViewModel, AuthorityFilterDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityDto, AuthorizationAuthorityViewModel>(defaultMemberValidation);

            #endregion

            #region AuthorityOperationGroup

            cfg.CreateMap<AuthorityOperationGroup, AuthorityOperationGroupEntity>(defaultMemberValidation)
                .ForMember(r => r.Parent, re => re.MapFrom(rs => rs.Parent.SysNo));
            cfg.CreateMap<AuthorityOperationGroupEntity, AuthorityOperationGroup>(defaultMemberValidation)
                .ForMember(re => re.Parent, r => r.MapFrom<AuthorityOperationGroupParentResolver>());
            cfg.CreateMap<AuthorityOperationGroup, AuthorityOperationGroupDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationGroupCmdDto, AuthorityOperationGroup>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationGroupDto, AuthorityOperationGroupViewModel>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationGroupDto, EditAuthorityOperationGroupViewModel>(defaultMemberValidation);
            cfg.CreateMap<EditAuthorityOperationGroupViewModel, AuthorityOperationGroupCmdDto>(defaultMemberValidation);

            #endregion

            #region AuthorityOperation

            cfg.CreateMap<AuthorityOperation, AuthorityOperationEntity>(defaultMemberValidation)
                .ForMember(c => c.Group, re => re.MapFrom(rs => rs.Group.SysNo));
            cfg.CreateMap<AuthorityOperationEntity, AuthorityOperation>(defaultMemberValidation)
                .ForMember(c => c.Group, re => re.MapFrom<AuthorityOperationGroupResolver>());
            cfg.CreateMap<AuthorityOperation, AuthorityOperationDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationCmdDto, AuthorityOperation>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationDto, AuthorityOperationViewModel>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationDto, EditAuthorityOperationViewModel>(defaultMemberValidation);
            cfg.CreateMap<EditAuthorityOperationViewModel, AuthorityOperationCmdDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationFilterViewModel, AuthorityOperationFilterDto>(defaultMemberValidation);

            #endregion

            #region AuthorityBindOperation

            cfg.CreateMap<ModifyAuthorityBindAuthorityOperationCmdDto, ModifyAuthorityAndAuthorityOperationBind>(defaultMemberValidation)
                .ForMember(c => c.Binds, ce => ce.MapFrom(cs => cs.Binds.Select(cm => new Tuple<Authority, AuthorityOperation>(cm.Item1.MapTo<Authority>(), cm.Item2.MapTo<AuthorityOperation>()))))
                .ForMember(c => c.UnBinds, ce => ce.MapFrom(cs => cs.UnBinds.Select(cm => new Tuple<Authority, AuthorityOperation>(cm.Item1.MapTo<Authority>(), cm.Item2.MapTo<AuthorityOperation>()))));
            cfg.CreateMap<AuthorityBindOperationFilterViewModel, AuthorityBindOperationFilterDto>(defaultMemberValidation);
            cfg.CreateMap<AuthorityOperationBindAuthorityFilterViewModel, AuthorityOperationBindAuthorityFilterDto>(defaultMemberValidation);

            #endregion

            #region UserAuthorize

            cfg.CreateMap<UserAuthorizeCmdDto, UserAuthorize>(defaultMemberValidation);
            cfg.CreateMap<UserAuthorize, UserAuthorizeEntity>(defaultMemberValidation)
                .ForMember(c => c.UserSysNo, ce => ce.MapFrom(cs => cs.User.SysNo))
                .ForMember(c => c.AuthoritySysNo, ce => ce.MapFrom(cs => cs.Authority.SysNo));
            cfg.CreateMap<UserAuthorizeEntity, UserAuthorize>(defaultMemberValidation)
                .ForMember(c => c.User, ce => ce.MapFrom<UserAuthorizeUserResolver>())
                .ForMember(c => c.Authority, ce => ce.MapFrom<UserAuthorizeAuthorityResolver>());

            #endregion

            #region Authentication

            cfg.CreateMap<AuthenticationCmdDto, Authentication>(defaultMemberValidation);

            #endregion
        }
    }

    #region Contact Resolver

    public class EntityContactResolver : IValueResolver<UserEntity, User, Contact>
    {
        public Contact Resolve(UserEntity source, User destination, Contact destMember, ResolutionContext context)
        {
            return new Contact(mobile: source.Mobile, email: source.Email, qq: source.QQ);
        }
    }

    public class DtoContactResolver : IValueResolver<EditUserViewModel, AdminUserCmdDto, Contact>
    {
        public Contact Resolve(EditUserViewModel source, AdminUserCmdDto destination, Contact destMember, ResolutionContext context)
        {
            return new Contact(mobile: source.Mobile, email: source.Email, qq: source.QQ);
        }
    }

    #endregion

    #region User->UserDto Converter

    public class UserToUserDtoConverter : ITypeConverter<User, UserDto>
    {
        public UserDto Convert(User source, UserDto destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            UserDto user = null;
            switch (source.UserType)
            {
                case UserType.管理账户:
                    user = ((AdminUser)source).MapTo<AdminUserDto>();
                    break;
            }
            return user;
        }
    }

    #endregion

    #region UserCmdDto->User Converter

    public class UserCmdDtoToUserConverter : ITypeConverter<UserCmdDto, User>
    {
        public User Convert(UserCmdDto source, User destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            User user = null;
            switch (source.UserType)
            {
                case UserType.管理账户:
                    user = ((AdminUserCmdDto)source).MapTo<AdminUser>();
                    break;
            }
            return user;
        }
    }

    #endregion

    #region UserDto->UserViewModel Converter

    public class UserDtoToUserViewModelConverter : ITypeConverter<UserDto, UserViewModel>
    {
        public UserViewModel Convert(UserDto source, UserViewModel destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            UserViewModel user = null;
            switch (source.UserType)
            {
                case UserType.管理账户:
                    user = ((AdminUserDto)source).MapTo<AdminUserViewModel>();
                    break;
            }
            return user;
        }
    }

    #endregion

    #region UserDto->EditAdminUserViewModel Converter

    public class UserDtoToEditUserViewModelConverter : ITypeConverter<UserDto, EditUserViewModel>
    {
        public EditUserViewModel Convert(UserDto source, EditUserViewModel destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            EditUserViewModel user = null;
            switch (source.UserType)
            {
                case UserType.管理账户:
                    user = ((AdminUserDto)source).MapTo<EditUserViewModel>();
                    break;
            }
            return user;
        }
    }

    #endregion

    #region UserViewModel->UserCmdDto Converter

    public class UserViewModelToUserCmdDtoConverter : ITypeConverter<UserViewModel, UserCmdDto>
    {
        public UserCmdDto Convert(UserViewModel source, UserCmdDto destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }
            UserCmdDto user = null;
            switch (source.UserType)
            {
                case UserType.管理账户:
                    user = ((AdminUserViewModel)source).MapTo<AdminUserCmdDto>();
                    break;
            }
            return user;
        }
    }

    #endregion

    #region Role Parent Resolver

    public class RoleParentResolver : IValueResolver<RoleEntity, Role, Role>
    {
        public Role Resolve(RoleEntity source, Role destination, Role destMember, ResolutionContext context)
        {
            return Role.CreateRole(source?.Parent ?? 0);
        }
    }

    #endregion

    #region AuthorityGroup Parent Resolver

    public class AuthorityGroupParentResolver : IValueResolver<AuthorityGroupEntity, AuthorityGroup, AuthorityGroup>
    {
        public AuthorityGroup Resolve(AuthorityGroupEntity source, AuthorityGroup destination, AuthorityGroup destMember, ResolutionContext context)
        {
            return AuthorityGroup.CreateAuthorityGroup(source?.Parent ?? 0);
        }
    }

    #endregion

    #region Authority Group Resolver

    public class AuthorityGroupResolver : IValueResolver<AuthorityEntity, Authority, AuthorityGroup>
    {
        public AuthorityGroup Resolve(AuthorityEntity source, Authority destination, AuthorityGroup destMember, ResolutionContext context)
        {
            return AuthorityGroup.CreateAuthorityGroup(source?.Group ?? 0);
        }
    }

    #endregion

    #region AuthorityOperationGroup Parent Resolver

    public class AuthorityOperationGroupParentResolver : IValueResolver<AuthorityOperationGroupEntity, AuthorityOperationGroup, AuthorityOperationGroup>
    {
        public AuthorityOperationGroup Resolve(AuthorityOperationGroupEntity source, AuthorityOperationGroup destination, AuthorityOperationGroup destMember, ResolutionContext context)
        {
            return AuthorityOperationGroup.CreateAuthorityOperationGroup(source?.Parent ?? 0);
        }
    }

    #endregion

    #region AuthorityOperation Group Resolver

    public class AuthorityOperationGroupResolver : IValueResolver<AuthorityOperationEntity, AuthorityOperation, AuthorityOperationGroup>
    {
        public AuthorityOperationGroup Resolve(AuthorityOperationEntity source, AuthorityOperation destination, AuthorityOperationGroup destMember, ResolutionContext context)
        {
            return AuthorityOperationGroup.CreateAuthorityOperationGroup(source?.Group ?? 0);
        }
    }

    #endregion

    #region UserAuthorize User Resolver

    public class UserAuthorizeUserResolver : IValueResolver<UserAuthorizeEntity, UserAuthorize, User>
    {
        public User Resolve(UserAuthorizeEntity source, UserAuthorize destination, User destMember, ResolutionContext context)
        {
            return User.CreateUser(source?.UserSysNo ?? 0);
        }
    }

    #endregion

    #region UserAuthorize Authority Resolver

    public class UserAuthorizeAuthorityResolver : IValueResolver<UserAuthorizeEntity, UserAuthorize, Authority>
    {
        public Authority Resolve(UserAuthorizeEntity source, UserAuthorize destination, Authority destMember, ResolutionContext context)
        {
            return Authority.CreateAuthority(source?.AuthoritySysNo ?? 0);
        }
    }

    #endregion
}
