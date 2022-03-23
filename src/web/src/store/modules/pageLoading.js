const state = () => ({
  isLoading: false,
  loadingText: '',
  isDisabled: false,
  defaultLoadingText: '正在加载...',
})
const getters = {
  //是否禁用操作
  isPageDisabled: (state) => state.isDisabled,
  //是否显示加载框
  isPageLoading: (state) => state.isLoading,
  //加载文字
  pageLoadingText: (state) => state.loadingText,
}
const mutations = {
  //显示加载框
  showPageLoading(state, loadingText, disableHandle = true) {
    if (loadingText && loadingText != '') {
      state.loadingText = loadingText
    } else {
      state.loadingText = state.defaultLoadingText
    }
    state.isLoading = true
    if (disableHandle) {
      state.isDisabled = true
    }
  },
  //隐藏加载框
  closePageLoading(state, enableHandle = true) {
    state.isLoading = false
    if (enableHandle) {
      state.isDisabled = false
    }
  },
  //禁用操作
  disablePageHandle(state) {
    state.isDisabled = true
  },
  //启用操作
  enablePageHandle(state) {
    state.isDisabled = false
  },
}
export default { state, getters, mutations }
