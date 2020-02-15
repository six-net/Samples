var initMenuLink=false;//是否初始化过链接菜单
var menuIsScroll = false;//菜单是否正在执行滚动

//页面初始化
$(function(){
	IndexLayout();
	BindEvent();
});


//页面整体布局
function IndexLayout(){
	//整体布局
	var winHeight = $(window).height();
	var headHeight=GetElementHeight("#index-head");
	var footHeight=GetElementHeight("#index-foot");
	var bodyHeight=winHeight-headHeight-footHeight;
	$("#index-body").height(bodyHeight);
}

//绑定页面事件
function BindEvent(){
	//页面窗口改变大小
	$(window).resize(function(){
		setTimeout(function(){
			IndexLayout();
			TabPageScroll(true,true);
			MenuScroll(1,true);
		},0);
	});
	
	//一级菜单滚动
	$("#menu-nav-con").mousewheel(function(eve, dalta) {
		if (menuIsScroll) {
			return;
		}
		menuIsScroll = true;
		MenuScroll(dalta);
	});
	//向上滚动点击事件
	$("#menu-srolltop-btn").click(function() {
		MenuScroll(1);
	});

	//向下滚动点击事件
	$("#menu-srolldown-btn").click(function() {
		MenuScroll(-1);
	});
	//一级菜单事件
	$("body").on("click","#index-menu-first #menu-nav-con ul li",function(){
		LevelOneMenuClick($(this));
	});
	//菜单链接
	$("body").on("click",".funcgroupmenu-list ul li",function(){
		MenuLinkClick($(this));
	});
	
	//显示隐藏菜单
	$("body").on('click','#btn_togglemenu',function(){
		ToggleMenu($(this));
	});
	
	//滚动页面标签
	$("body").on('click','#btn_prevtabitem',function(){
		TabPageScroll(false);
	});
	$("body").on('click','#btn_nexttabitem',function(){
		TabPageScroll(true);
	});
	//关闭标签
	$("body").on('click','#pagetabs-pageitem-container li .tab-close-btn',function(){
		var tabTitEle=$(this).parent();
		ClosePageByTabTitle(tabTitEle)
	});
	//标签点击
	$("body").on('click','#pagetabs-pageitem-container li',function(){
		SelectPageByTabTitle($(this));
	});
	//关闭当前页
	$("body").on('click',"#pagetabs-titleselect-closecurrent",CloseCurrentPage);
	//关闭所有
	$("body").on('click',"#pagetabs-titleselect-closeall",CloseAllPages);
	//关闭其它
	$("body").on('click',"#pagetabs-titleselect-closeother",CloseOtherAllPages);
	//刷新当前页
	$("body").on("click","#tabpagetitle-refreshcurrentpage",RefreshCurrentPage);
}

//菜单滚动
function MenuScroll(dalta,resize) {
    var menuConHeight = $("#menu-nav-con").height();
    var ulEle = $("#menu-nav-con>ul");
    var ulEleHeight = ulEle.height();
    var scrollUpBtnHeight=$("#menu-srolltop-btn").outerHeight();
    var scrollDownHeight=$("#menu-srolldown-btn").outerHeight();
    var menuShowHeight = menuConHeight-scrollUpBtnHeight-scrollDownHeight; //菜单显示区域高度
    if(ulEleHeight<=menuShowHeight){
    	return;
    }
    var maxOffsetValue=ulEleHeight-menuShowHeight-19;
    var nowOffsetValue=ulEle.position().top;
    if(dalta>0&&Math.abs(nowOffsetValue)>maxOffsetValue){
    	ulEle.css("top", -maxOffsetValue + "px");
    	return;
    }
    if(resize){
    	return;
    }
    nowOffsetValue += dalta * 180;
    nowOffsetValue = nowOffsetValue > 0 ? 0 : nowOffsetValue;
    nowOffsetValue = Math.abs(nowOffsetValue)>maxOffsetValue ? -maxOffsetValue : nowOffsetValue;
    ulEle.css("top", nowOffsetValue + "px");
    menuIsScroll = false;
};

//收起/展开菜单
function ToggleMenu(btn){
	var menuEle=$("#index-menu");
	var preShowClass='';
	if(menuEle.hasClass('close')){
		preShowClass=menuEle.data('prev-showclass');
		menuEle.removeClass('close').addClass(preShowClass);
		btn.addClass('micon-shrink-right').removeClass('micon-spread-left').attr('title','隐藏菜单');
	}else{
		if(menuEle.hasClass('FullShow')){
			preShowClass="FullShow";
			menuEle.data('prev-showclass',"FullShow");
		}
		menuEle.removeClass(preShowClass).addClass('close');
		btn.addClass('micon-spread-left').removeClass('micon-shrink-right').attr('title','显示菜单');
	}
	setTimeout(function(){
		TabPageScroll(true,true);
	},300)
}

//一级菜单点击
function LevelOneMenuClick(menuEle){
	if(menuEle.hasClass('active')){
		return;
	}
	var menuList=menuEle.find('.menu-list').html();
	$("#index-menu-second-list").html(menuList);
	InitMenuLink();
	$("#menu-nav-con").find('.active').removeClass('active');
	menuEle.addClass('active');
	$("#index-menu").addClass('FullShow')
	$("#index-menu-second").addClass("Show");
}
//菜单链接点击
function MenuLinkClick(menuEle){
	$("#index-menu-second-list").find('.active').removeClass('active');
	menuEle.addClass("active")
	var url=menuEle.attr('action');
	var title=menuEle.text();
	var page=OpenPage({
		url:url,
		title:title
	});
}

//初始化菜单链接
function InitMenuLink(){
	if(initMenuLink){
		$( "#index-menu-second-list" ).accordion( "destroy" );
		initMenuLink=false;
	}
	if($( "#index-menu-second-list .funcgroupmenu-title").length<=0){
		return;
	}
	var icons = {
      header: "micon micon-right",
      activeHeader: "micon micon-down"
    };
	$( "#index-menu-second-list" ).accordion({
		icons: icons,
		heightStyle: "content"
	});
	initMenuLink=true;
}

//页面内容Tab滚动
function TabPageScroll(toRight,resize){
	var titleConEle=$("#pagetabs-pageitem-container");
	var conWidth=titleConEle.width();
	var left=titleConEle.position().left;
	if(!toRight&&left>=0){
		return;
	}
	var allItemWidth=0;
	titleConEle.find('li').each(function(i,e){
		allItemWidth+=$(e).outerWidth();
	});
	if(allItemWidth<=conWidth){
		titleConEle.css('left','0px');
		return;
	}
	if(toRight&&Math.abs(left)>=(allItemWidth-conWidth)){
		left=-(allItemWidth-conWidth);
		titleConEle.css('left',left+'px');
		return;
	}
	if(resize){
		return;
	}
	if(toRight){
		left-=(conWidth-100);
		if(Math.abs(left)>allItemWidth-conWidth){
			left=-(allItemWidth-conWidth);
		}
	}
	else{
		left+=(conWidth-100);
		if(left>0){
			left=0;
		}
	}
	titleConEle.css('left',left+'px');
}
