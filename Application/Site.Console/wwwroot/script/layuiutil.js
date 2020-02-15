//监听Tab事件
function ListenTabEvent(filter,func){
    layui.element.on('tab('+filter+')', function (data) {
        func(data);
    });
};
//触发tab事件
function TabChange(id,tabId) {
    layui.element.tabChange(id, tabId);
}

//输出模板内容
function OutPutTemplateScript(id,script){
    var newElement=document.createElement("script");
    newElement.setAttribute("type","text/html");
    newElement.setAttribute("id",id);
    newElement.innerHTML=script;
    document.body.appendChild(newElement);
}

