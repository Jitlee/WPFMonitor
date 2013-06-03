USE [WPFMonitor]

-- 创建图库表
IF NOT EXISTS (SELECT NULL FROM dbo.SysObjects WHERE ID = object_id(N't_GalleryClassification')) 
BEGIN
	CREATE TABLE [t_GalleryClassification](
		[Id] [int] NOT NULL PRIMARY KEY,
		[Name] [nvarchar](16) NOT NULL,
		[Description] [nvarchar](100) NULL,
		[Sort] [int] NOT NULL DEFAULT (99999))

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N't_GalleryClassification', @level2type=N'COLUMN', @level2name=N'Id'

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N't_GalleryClassification', @level2type=N'COLUMN', @level2name=N'Name'

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N't_GalleryClassification', @level2type=N'COLUMN', @level2name=N'Description'

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序字段' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N't_GalleryClassification', @level2type=N'COLUMN', @level2name=N'Sort'

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图库分类表' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N't_GalleryClassification'

END
GO
IF NOT EXISTS(SELECT NULL FROM [t_GalleryClassification] WHERE [Id]=4)
BEGIN
	INSERT INTO [t_GalleryClassification]
			   ([Id]
			   ,[Name]
			   ,[Description]
			   ,[Sort])
		 VALUES
			   (4
			   ,'仪表'
			   ,NULL
			   ,4)
END

GO
IF NOT EXISTS(SELECT NULL FROM [t_GalleryClassification] WHERE [Id]=6)
BEGIN
	INSERT INTO [t_GalleryClassification]
			   ([Id]
			   ,[Name]
			   ,[Description]
			   ,[Sort])
		 VALUES
			   (6
			   ,'电力符号'
			   ,NULL
			   ,6)
END

GO
IF NOT EXISTS(SELECT NULL FROM [t_GalleryClassification] WHERE [Id]=6)
BEGIN
	INSERT INTO [t_GalleryClassification]
			   ([Id]
			   ,[Name]
			   ,[Description]
			   ,[Sort])
		 VALUES
			   (6
			   ,'电子符号'
			   ,NULL
			   ,6)
END

GO
IF NOT EXISTS(SELECT NULL FROM [t_GalleryClassification] WHERE [Id]=7)
BEGIN
	INSERT INTO [t_GalleryClassification]
			   ([Id]
			   ,[Name]
			   ,[Description]
			   ,[Sort])
		 VALUES
			   (7
			   ,'电气符号'
			   ,NULL
			   ,7)
END

GO
IF NOT EXISTS(SELECT NULL FROM [t_GalleryClassification] WHERE [Id]=8)
BEGIN
	INSERT INTO [t_GalleryClassification]
			   ([Id]
			   ,[Name]
			   ,[Description]
			   ,[Sort])
		 VALUES
			   (8
			   ,'其他'
			   ,NULL
			   ,8)
END

declare @ControlNum int;--查询控件数量，用于判断是否已经添加
set @ControlNum=0;
declare @AddControl varchar(50);--添加控件名称
declare @ControlID int;

--添加“电力符号001”
set @AddControl='dlfh01'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh01','电力符号','电力符号001');

		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			--电容颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'CapacitiveColor','#FFFA0000','电容颜色');
			--电容宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'CapacitiveWidth','1','电容宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 6,'LineWidth','1','线路宽度');
		end
end
--添加“电力符号002”
set @AddControl='dlfh02'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh02','电力符号','电力符号002');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			--电容颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'CapacitiveColor','#FFFA0000','电容颜色');
			--电容宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'CapacitiveWidth','1','电容宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 6,'LineWidth','1','线路宽度');
		end
end

--添加“电力符号03-- 接地线”
set @AddControl='dlfh03'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh03','电力符号','接地线');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');			
			--接地线颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'GroundWireColor','#FFFA0000','接地线颜色');
			--接地线宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'GroundWireWidth','1','接地线宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','线路宽度');
		end
end

--添加“电力符号04-- 接地线”
set @AddControl='dlfh04'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh04','电力符号','接地线');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');			
			--接地线颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'GroundWireColor','#FFFA0000','接地线颜色');
			--接地线宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'GroundWireWidth','1','接地线宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','线路宽度');
		end
end

--添加“电力符号05-- 接地线”
set @AddControl='dlfh05'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh05','电力符号','接地线');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');			
			--接地线颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'GroundWireColor','#FFFA0000','接地线颜色');
			--接地线宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'GroundWireWidth','1','接地线宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','线路宽度');
		end
end

--添加“电力符号06-- 接地线”
set @AddControl='dlfh06'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh06','电力符号','接地线');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');			
			--接地线颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'GroundWireColor','#FFFA0000','接地线颜色');
			--接地线宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'GroundWireWidth','1','接地线宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','线路宽度');
		end
end


--添加“电力符号07-- 接地线”
set @AddControl='dlfh07'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh07','电力符号','接地线');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');			
			--接地线颜色
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'GroundWireColor','#FFFA0000','接地线颜色');
			--接地线宽度
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'GroundWireWidth','1','接地线宽度');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FF0B0A0A','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','线路宽度');
		end
end

--添加“电力符号08-- 接地线”
set @AddControl='dlfh08'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh08','电力符号','电力符号08');
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'LineColor','#FFED1212','线路颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'LineWidth','1','线路宽度');
		end
end


--添加“电力符号09”
set @AddControl='dlfh09'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh09','电力符号','母线02');

		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			--是否立体
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'ISLT','False','是否立体');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','母线颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','母线宽度');
		end
end

--添加“电力符号010”
set @AddControl='dlfh10'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh10','电力符号','母线02');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电容器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			--是否立体
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'ISLT','False','是否立体');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','母线颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','母线宽度');
		end
end

--添加“电力符号011”
set @AddControl='dlfh11'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh11','电力符号','电抗器1');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电抗器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','电抗器颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','电抗器宽度');
		end
end

--添加“电力符号012”
set @AddControl='dlfh12'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh12','电力符号','电抗器2');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电抗器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','电抗器颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','电抗器宽度');
		end
end

--添加“电力符号013”
set @AddControl='dlfh13'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh13','电力符号','电抗器3');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电抗器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','电抗器颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','电抗器宽度');
		end
end

--添加“电力符号014”
set @AddControl='dlfh14'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.Dlfh14','电力符号','电抗器4');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电抗器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','电抗器颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','电抗器宽度');
		end
end

--添加“电力符号015”
set @AddControl='Dlfh15'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.'+@AddControl ,'电力符号','发电机1');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电抗器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','发电机颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'LineWidth','1','发电机宽度');
		end
end
--添加“电力符号016”
set @AddControl='Dlfh16'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'5','MonitorSystem.Dlfh.'+@AddControl ,'电力符号','等值系统');
		
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','电抗器','设备名称');
			--电压等级
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Voltagelevel','10','电压等级');
			

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'LineColor','#FFED1212','系统颜色');
			
		end
end

/*****************************************
控件	电力电子
******************************************/
--添加控件	电力电子01 
set @AddControl='Dldz01'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.Dldz01','电力电子','电力电子01');
end
--添加控件	电力电子02
set @AddControl='Dldz02'
select @ControlNum=count(ControlID) from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.Dldz02','电力电子','电力电子02');
end

--添加控件	电力电子03
set @AddControl='Dldz03'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子03');
end

--添加控件	电力电子04
set @AddControl='Dldz04'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子04');
end

--添加控件	电力电子05
set @AddControl='Dldz05'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子05');
end

--添加控件	电力电子06
set @AddControl='Dldz06'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子06');
end

--添加控件	电力电子07
set @AddControl='Dldz07'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子07');
end

--添加控件	电力电子08
set @AddControl='Dldz08'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子08');
end

--添加控件	电力电子09
set @AddControl='Dldz09'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子09');
end

--添加控件	电力电子10
set @AddControl='Dldz10'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子10');
end

--添加控件	电力电子11
set @AddControl='Dldz11'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子11');
end

--添加控件	电力电子12
set @AddControl='Dldz12'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子12');
end

--添加控件	电力电子13
set @AddControl='Dldz13'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子13');
end

--添加控件	电力电子14
set @AddControl='Dldz14'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子14');
end

--添加控件	电力电子15
set @AddControl='Dldz15'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子15');
end

--添加控件	电力电子16
set @AddControl='Dldz16'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子16');
end

--添加控件	电力电子17
set @AddControl='Dldz17'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子17');
end

--添加控件	电力电子18
set @AddControl='Dldz18'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子18');
end

--添加控件	电力电子19
set @AddControl='Dldz19'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子19');
end

--添加控件	电力电子20
set @AddControl='Dldz20'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子20');
end

--添加控件	电力电子21
set @AddControl='Dldz21'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子21');
end

--添加控件	电力电子22
set @AddControl='Dldz22'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子22');
end

--添加控件	电力电子23
set @AddControl='Dldz23'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子23');
end

--添加控件	电力电子24
set @AddControl='Dldz24'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子24');
end

--添加控件	电力电子17
set @AddControl='Dldz25'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子25');
end

--添加控件	电力电子26
set @AddControl='Dldz26'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'6','MonitorSystem.Dldz.'+@AddControl,'电力电子','电力电子26');
end
/*****************************************
添加控件	电气符号
******************************************/

--添加控件	电气符号01
set @AddControl='Dqfh01'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号01');
end
--添加控件	电气符号02
set @AddControl='Dqfh02'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号02');
end

--添加控件	电气符号03
set @AddControl='Dqfh03'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号03');
end

--添加控件	电气符号04
set @AddControl='Dqfh04'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号04');
end

--添加控件	电气符号05
set @AddControl='Dqfh05'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号05');
end

--添加控件	电气符号06
set @AddControl='Dqfh06'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号06');
end

--添加控件	电气符号07
set @AddControl='Dqfh07'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号07');
end

--添加控件	电气符号08
set @AddControl='Dqfh08'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号08');
end

--添加控件	电气符号09
set @AddControl='Dqfh09'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号09');
end

--添加控件	电气符号10
set @AddControl='Dqfh10'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号10');
end

--添加控件	电气符号11
set @AddControl='Dqfh11'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号11');
end

--添加控件	电气符号12
set @AddControl='Dqfh12'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号12');
end

--添加控件	电气符号13
set @AddControl='Dqfh13'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号13');
end

--添加控件	电气符号14
set @AddControl='Dqfh14'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号14');
end

--添加控件	电气符号15
set @AddControl='Dqfh15'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号15');
end

--添加控件	电气符号16
set @AddControl='Dqfh16'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号16');
end

--添加控件	电气符号17
set @AddControl='Dqfh17'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号17');
end

--添加控件	电气符号18
set @AddControl='Dqfh18'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号18');
end

--添加控件	电气符号19
set @AddControl='Dqfh19'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号19');
end

--添加控件	电气符号20
set @AddControl='Dqfh20'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号20');
end

--添加控件	电气符号21
set @AddControl='Dqfh21'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号21');
end

--添加控件	电气符号22
set @AddControl='Dqfh22'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'7','MonitorSystem.Dqfh.'+@AddControl,'电气符号','电气符号22');
end

--update t_control set controltypeName='电气符号',controlCaption='电气符号02' where controlname='Dqfh02';
--update t_control set controltypeName='电气符号',controlCaption='电气符号01' where controlname='Dqfh01';


--添加“仪表1”
set @AddControl='Meter1'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter1','仪表','仪表1');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','仪表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','6','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','4','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表2”
set @AddControl='Meter2'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter2','仪表','仪表2');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','仪表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','60','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'Scale','10','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表3”
set @AddControl='Meter3'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter3','仪表','指示表1');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','指示表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','100','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','100','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','5','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','3','副刻度');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表4”
set @AddControl='Meter4'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter4','仪表','仪表4');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'MainScale','6','主刻度');
		
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表5”
set @AddControl='Meter5'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter5','仪表','仪表5');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'MainScale','6','主刻度');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表6”
set @AddControl='Meter6'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter6','仪表','仪表6');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','仪表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','6','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','4','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表7”
set @AddControl='Meter7'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter7','仪表','仪表7');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'MainScale','3','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'ViceScale','4','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表8”
set @AddControl='Meter8'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter8','仪表','仪表8');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'MainScale','6','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'ViceScale','4','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表9”
set @AddControl='Meter9'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter9','仪表','仪表9');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'MainScale','6','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表10”
set @AddControl='Meter10'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter10','仪表','仪表10');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','Voltage','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','60','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','6','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','4','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表11”
set @AddControl='Meter11'
select @ControlNum=count(ControlID) ,@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter11','仪表','仪表11');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','仪表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','100','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','10','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','1','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'NormalFrom','40','正常区间从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 9,'NormalTo','60','正常区间到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 10,'Warring1From','20','警告区间1从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 11,'Warring1To','40','警告区间1到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 12,'Warring2From','60','警告区间2从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 13,'Warring2To','80','警告区间2到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 14,'Exception1From','0','异常区间1从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 15,'Exception1To','20','异常区间1到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 16,'Exception2From','80','异常区间2从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 17,'Exception2To','100','异常区间2到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 18,'LabelColor','#FF000000','标签颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 19,'DialPlateBackColor','#FFFFFFFF','表盘颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 20,'CalibrationColor','#FF0000FF','刻度文本颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 21,'NormalColor','#FF00E700','正常区间颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 22,'WarringColor','#FFFFFF00','警告区间颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 23,'ExceptionColor','#FFFF0000','异常区间颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 24,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表12”
set @AddControl='Meter12'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter12','仪表','仪表12');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','仪表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','100','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','10','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','1','副刻度');
		
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'BackColor','#FFCECBC5','背景颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 9,'LabelColor','#FF000000','标签颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 10,'DialPlateBackColor','#FFFFFFFF','表盘颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 11,'DialPlateBorlderColor','#FF008040','表盘轮廓颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 12,'CalibrationColor','#FF0000FF','刻度文本颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 13,'CalibrationColor','#FF000000','刻度颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 14,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表13”
set @AddControl='Meter13'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'4','MonitorSystem.Gallery.Meter.Meter13','仪表','仪表13');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Text','仪表','标签');
		--电压等级
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'Value','0','当前值');
		--电容颜色
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'Maximum','100','最大值');
		--电容宽度
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'Minimum','0','最小值');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'DecimalDigits','0','小数位数');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'MainScale','10','主刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'ViceScale','1','副刻度');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'NormalFrom','40','正常区间从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 9,'NormalTo','60','正常区间到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 10,'Warring1From','20','警告区间1从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 11,'Warring1To','40','警告区间1到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 12,'Warring2From','60','警告区间2从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 13,'Warring2To','80','警告区间2到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 14,'Exception1From','0','异常区间1从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 15,'Exception1To','20','异常区间1到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 16,'Exception2From','80','异常区间2从');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 17,'Exception2To','100','异常区间2到');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 18,'LabelColor','#FF000000','标签颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 19,'DialPlateBackColor','#FFFFFFFF','表盘颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 20,'CalibrationColor','#FF0000FF','刻度文本颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 21,'NormalColor','#FF00E700','正常区间颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 22,'WarringColor','#FFFFFF00','警告区间颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 23,'ExceptionColor','#FFFF0000','异常区间颜色');

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 24,'ScaleColor','#FF000000','刻度颜色');
	end
end

--添加“仪表13”
set @AddControl='OtherRealTime'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'8','MonitorSystem.Other.RealTimeT','其它','实时曲线');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		-- X轴
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'XISSGShow','true','是否X轴栅格显示');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'XMainNumber','3','X主分度数');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'XMainColor','#FF8E8EC1','X主分度颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'XPriNumber','3','X次分度数');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'XPriColor','#FF12C0D5','X度次分颜色');
		
		--Y轴
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'YISSGShow','true','是否Y轴栅格显示');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'YMainNumber','3','Y主分度数');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 8,'YMainColor','#FFC8A4A4','Y主分度颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 9,'YPriNumber','3','Y次分度数');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 10,'YPriColor','#FF58DB1B','Y度次分颜色');
		--其它颜色

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 11,'ISShowBorder','True','显示边框');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 29,'BorderColor','#FFE1BFBF','边框');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 12,'ISShowGridBack','True','显示背景');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 30,'GridBackColor','#FFFFFFFF','背景颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 13,'ISShowCursor','True','显示游标');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 31,'CursorColor','#FF3C433B','游标颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 14,'ISShowTime','True','显示时间');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 32,'TimeColor','#FFE8DCDC','时间颜色');
		

		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 15,'UsePerZB','false','采用百分比坐标');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 16,'NoUseDataMove','false','无效数据移出');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 17,'DoubleClickShowSet','true','双击显示设置框');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 18,'RightShowYZB','false',' 右显示Y轴坐标');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 19,'MultiXZShow','false','多X轴显示');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 20,'MultiYZShow','false','多Y轴显示');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 33,'IsShowLegend','true','显示图例');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 21,'ShowLegend','111111111','图例信息');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 22,'InfoLWidth','10','信息栏宽度');

		--放缩设置
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 23,'MouseDrawEnlare','false','鼠标拖动放大');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 24,'XZEnlare','false','X轴放大');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 25,'YZEnlare','false','Y轴放大');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 26,'MouseDrawMove','true','鼠标拖动移动');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 27,'XZMove','true','X轴移动');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 28,'YZMove','true','Y轴移动');
	end
--这个属性还需要添加一个表:
--因为它有多个线条
--曲线类型、名称、取值、类型(直线、阶梯线)、样式(),标记(不画点、。。。。)
--颜色 低值、高值、小数位、选择设备(从哪里取值)
--显示格式（年月日时分秒）、时间:
--时间设置	时间长度	采样周期



--添加“ToolTip”
set @AddControl='ToolTip'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'-1','MonitorSystem.MonitorSystemGlobal.ToolTipControl','提示控件','ToolTip');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Stroke','#FFC5B78A','边线颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'StrokeThickness','1','边线粗细');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'CornerRadius','10','圆角度');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'FromColor','#FFFFE7AF','起始填充颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'ToColor','#FFFFE7AF','终止填充颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'FillDirection','0','填充方向');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'BackImage','','填充图片');
	end
end

--BackgroundControl
set @AddControl='BackgroundControl'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl
	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'2','Text.jpg','组态控件','背景框');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID
begin
	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'Stroke','#FF646464','边线颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'StrokeThickness','1','边线粗细');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'CornerRadius','10','圆角度');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 4,'FromColor','#FF58504E','起始填充颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 5,'ToColor','#FF262920','终止填充颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 6,'FillDirection','0','填充方向');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 7,'BackImage','','填充图片');
	end
end


set @AddControl='CalloutControl'
select @ControlNum=count(ControlID),@ControlID=ControlID from t_control where controlname=@AddControl group by ControlID
if @ControlNum = 0
begin
	print '添加控件'
	print @AddControl

	insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
	values(@AddControl,'8','MonitorSystem.Other.CalloutControl','其它','云');

	set @ControlID=0;
	select @ControlID=max(controlid)  from t_control
end
else
	delete [t_ControlProperty] where [ControlID] = @ControlID

	if @ControlID > 0
	begin
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 1,'CStroke','#FF000000','边线颜色');
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 2,'CStrokeThickness','1','边线粗细');
		
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'CFillColor','#FF929A81','填充颜色');
		
		INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
			 VALUES(@ControlID, 3,'CText','','标题');
	end
end




