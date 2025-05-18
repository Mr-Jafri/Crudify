
----Insert Users
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8f848d0d-88f9-4c27-b7a6-048e4e9f45ee', N'Daniyal', N'Ali', N'sdjafri', N'SDJAFRI', N'sdjafri@calrom.com', N'SDJAFRI@CALROM.COM', 1, N'AQAAAAEAACcQAAAAEGGzASDhpQymKC53qIjFytmO3QGMsJKG+gAulEFNHAA61UMq03voMBzz/gcNDUkB5g==', NULL, NULL, NULL, 1, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8d4a3a7a-d19d-4f8e-ba3b-1aeb817e1dae', N'Zeeshan', N'Ali', N'sjafri', N'SJAFRI', N'sjafri@calrom.com', N'SJAFRI@CALROM.COM', 1, N'AQAAAAEAACcQAAAAEGGzASDhpQymKC53qIjFytmO3QGMsJKG+gAulEFNHAA61UMq03voMBzz/gcNDUkB5g==', NULL, NULL, NULL, 1, 0, NULL, 0, 0)

----Insert Roles
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'7b7beda6-fdb6-498a-96f8-12eab14dd24c', N'Admin', N'ADMIN', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6deb8d25-3507-4022-bc0d-6108c9e5aa86', N'User', N'USER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'61b94ba4-4c16-40b2-85b8-93382d913d5a', N'Manager', N'MANAGER', NULL)

----Insert User Roles
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8d4a3a7a-d19d-4f8e-ba3b-1aeb817e1dae', N'7b7beda6-fdb6-498a-96f8-12eab14dd24c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8f848d0d-88f9-4c27-b7a6-048e4e9f45ee', N'6deb8d25-3507-4022-bc0d-6108c9e5aa86')

