var tabPages={};
var tabUrlPages={};
var pageNum=0;
var currentPageNum=0;

//打开页面
function OpenPage(options){
	if(!options){
		return null;
	}
	var url=$.trim(options.url);
	var title=$.trim(options.title);
	if(url==""){
		return null;
	}
	if(title==""){
		title="新页面";
	}
	if(tabUrlPages[url]){
		var nowPageNum=tabUrlPages[url];
		var nowTabPage=tabPages[nowPageNum];
		if(nowTabPage){
			nowTabPage.tab.click();
			RefreshPage(nowPageNum);
			return;
		}
	}
	pageNum++;
	var titEle=$('<li><span class="title-name">'+title+'</span><i class="micon tab-close-btn">ဆ</i></li>');
	titEle.data('page-index',pageNum);
	$("#pagetabs-pageitem-container").append(titEle);
	
	var pageItem = document.createElement("div");
	pageItem.className = "page-item";
	var iframeEle = document.createElement("iframe");
	iframeEle.setAttribute("src", url);
	iframeEle.setAttribute("border", "0");
	iframeEle.setAttribute("style", "border:none");
	iframeEle.setAttribute("frameborder","0");
	pageItem.appendChild(iframeEle);
	pageItem=$(pageItem);
	pageItem.hide();
	$("#pagetabs-bodycontainer").append(pageItem);
	var tabPageObj={
		page: $(pageItem),
		tab: titEle,
		src: url
	};
	tabPages[pageNum] = tabPageObj;
	tabUrlPages[url]=pageNum;
	titEle.click();
	return tabPageObj;
}

//根据页面标签关闭页面
function ClosePageByTabTitle(tabTitle){
	if(!tabTitle){
		return;
	}
	var pageNum=parseInt(tabTitle.data("page-index"));
	ClosePageByPageIndex(pageNum);
}

//根据页面编号关闭页面
function ClosePageByPageIndex(pageNum){
	var tabPage=tabPages[pageNum];
	if(!tabPage){
		return;
	}
	var nextEle=tabPage.tab.next('li');
	var prevEle=tabPage.tab.prev('li');
	RemoveTabPage(pageNum);
	TabPageScroll(true,true);
	if(currentPageNum!=pageNum){
		return;
	}
	if(nextEle&&nextEle.length>0){
		nextEle.click();
	}
	else if(prevEle&&prevEle.length>0){
		prevEle.click();
	}
}

//移除标签页
function RemoveTabPage(pageNum){
	var tabPage=tabPages[pageNum];
	if(!tabPage){
		return;
	}
	tabPage.page.remove();
	tabPage.tab.remove();
	tabUrlPages[tabPage.src]=null;
	tabPages[pageNum]=null;
}

//关闭其它的页面
function CloseOtherAllPages(){
	for(var p in tabPages){
		if(p==currentPageNum){
			continue;
		}
		RemoveTabPage(p);
	}
	TabPageScroll(true,true);
}

//关闭页面
function CloseAllPages(){
	for(var p in tabPages){
		ClosePageByPageIndex(p);
		RemoveTabPage(p);
	}
	TabPageScroll(true,true);
	currentPageNum=0;
}

//关闭当前页
function CloseCurrentPage(){
	ClosePageByPageIndex(currentPageNum);
}

//根据页面标签选择页面
function SelectPageByTabTitle(tabTitle){
	if(!tabTitle){
		return;
	}
	var pageNum=parseInt(tabTitle.data("page-index"));
	SelectPageByPageIndex(pageNum);
}

//根据页面编号选择页面
function SelectPageByPageIndex(pageNum){
	var tabPage=tabPages[pageNum];
	if(!tabPage){
		return;
	}
	var currentTabPage=tabPages[currentPageNum];
	if(pageNum==currentPageNum){
		return;
	}
	if(currentTabPage){
		currentTabPage.page.hide();
	}
	currentPageNum=pageNum;
	$('.current-tab').removeClass('current-tab');
	tabPage.tab.addClass('current-tab');
	tabPage.page.show();
	//$("#pagetabs-bodycontainer").append(tabPage.page);
}

//刷新当前页面
function RefreshCurrentPage() {
	RefreshPage(currentPageNum);
};

//刷新页面
function RefreshPage(pageNum){
	if (pageNum <= 0) {
		return;
	}
	var tabPage = tabPages[pageNum];
	if (!tabPage) {
		return;
	}
	var iframeEle = $(tabPage.page).find("iframe")[0];
	iframeEle.src = tabPage.src;
}

//重定向页面
function RedirectTabPage(pageNum,newUrl,newTitle){
	var tabPage=tabPages[pageNum];
	if(!tabPage||$.trim(newUrl)==""||newUrl==tabPage.src){
		return;
	}
	var nowPageNum=tabUrlPages[newUrl];
	var nowTabPage=tabPages[nowPageNum];
	if(nowTabPage){ //如果当前已存在新地址的页面就切换到指定页面
		ClosePageByPageIndex(pageNum);
		nowTabPage.tab.click();
		return;
	}
	tabUrlPages[tabPage.src]=null;
	if($.trim(newTitle)!=""){
		tabPage.tab.find('.title-name').text(newTitle);
	}
	var iframeEle = $(tabPage.page).find("iframe")[0];
    iframeEle.src = newUrl;
    tabUrlPages[newUrl] = pageNum;
    tabPage.src = newUrl;
}

//重定向当前页面
function RedirectCurrentPage(newUrl,newTitle){
	RedirectTabPage(currentPageNum,newUrl,newTitle);
}
