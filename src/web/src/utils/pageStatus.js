class PageStatusManager {
  isLoading = false
  loadingText = ''
  isDisabled = false
  defaultLoadingText = '正在加载...'

  //显示加载框
  showPageLoading(loadingText, disableHandle = true) {
    if (loadingText && loadingText != '') {
      this.loadingText = loadingText
      //this.vm.$set(this.vm.pageStatus, 'loadingText', loadingText)
    } else {
      this.loadingText = this.defaultLoadingText
      //this.vm.$set(this.vm.pageStatus, 'loadingText', this.defaultLoadingText)
    }
    this.isLoading = true
    //this.vm.$set(this.vm.pageStatus, 'isLoading', true)
    if (disableHandle) {
      this.isDisabled = true
    }
  }
  //隐藏加载框
  closePageLoading(enableHandle = true) {
    this.isLoading = false
    if (enableHandle) {
      this.isDisabled = false
    }
  }
  //禁用操作
  disablePageHandle() {
    this.isDisabled = true
  }
  //启用操作
  enablePageHandle() {
    this.isDisabled = false
  }
}

//获取页面状态管理对象
export function getPageStatus() {
  return new PageStatusManager()
}
