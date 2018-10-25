import Vue from 'vue';
import iView from 'iview';
import {router} from './router/index';
import {appRouter} from './router/router';
import store from './store';
import App from './app.vue';
import '@/locale';
import 'iview/dist/styles/iview.css';
import VueI18n from 'vue-i18n';
import util from './libs/util';
import AppConsts from './libs/appconst';

util.ajax.get('/MyAbpUserConfiguration/GetAll').then(result => {
    Vue.use(VueI18n);
    Vue.use(iView);

    window.abp = $.extend(true, abp, result.data.result);
    
    Vue.prototype.L = function (text, ...args) {
        let localizedText = window.abp.localization.localize(text, AppConsts.localization.defaultLocalizationSourceName);
        if (!localizedText) {
            localizedText = text;
        }
        if (!args || !args.length) {
            return localizedText;
        }
        args.unshift(localizedText);
        return abp.utils.formatString.apply(this, args)
    }

    Vue.filter('l', function (value) {
        if (!value) return ''
        return window.abp.localization.localize(value, AppConsts.localization.defaultLocalizationSourceName);
    });

    new Vue({
        el: '#app',
        router: router,
        store: store,
        render: h => h(App),
        data: {
            currentPageName: ''
        },
        mounted() {
            this.currentPageName = this.$route.name;
            // Display a list of open pages
            this.$store.commit('setOpenedList');
            this.$store.commit('initCachepage');

            //Filtering admin menu
            this.$store.commit('updateMenulist');
        },
        created() {
            let tagsList = [];
            appRouter.map((item) => {
                if (item.children.length <= 1) {
                    tagsList.push(item.children[0]);
                } else {
                    tagsList.push(...item.children);
                }
            });

            this.$store.commit('setTagsList', tagsList);
            abp.message.info = (message, title) => {
                this.$Modal.info({
                    title: title,
                    content: message
                })
            };

            abp.message.success = (message, title) => {
                this.$Modal.success({
                    title: title,
                    content: message
                })
            };

            abp.message.warn = (message, title) => {
                this.$Modal.warning({
                    title: title,
                    content: message
                })
            };

            abp.message.error = (message, title) => {
                
                this.$Modal.error({
                    title: title,
                    content: message
                })
            };

            abp.message.confirm = (message, titleOrCallback, callback) => {
                var userOpts = {
                    content: message
                };
                if ($.isFunction(titleOrCallback)) {
                    callback = titleOrCallback;
                } else if (titleOrCallback) {
                    userOpts.title = titleOrCallback;
                };
                userOpts.onOk = callback || function () { };
                this.$Modal.confirm(userOpts);
            }

            abp.notify.success = (message, title, options) => {
                this.$Notice.success({
                    title: title,
                    desc: message
                })
            };

            abp.notify.info = (message, title, options) => {
                this.$Notice.info({
                    title: title,
                    desc: message
                })
            };

            abp.notify.warn = (message, title, options) => {
                this.$Notice.warning({
                    title: title,
                    desc: message
                })
            };

            abp.notify.error = (message, title, options) => {
                this.$Notice.error({
                    title: title,
                    desc: message
                })
            };
            abp.randomNumber=()=>{//根据当前时间和随机数生成流水号
                let now = new Date();
                let month = abp.dataleftComplete(now.getMonth() + 1);
                let day = abp.dataleftComplete(now.getDate());
                let hour = abp.dataleftComplete(now.getHours());
                let minutes = abp.dataleftComplete(now.getMinutes());
                let seconds = abp.dataleftComplete(now.getSeconds());
                return now.getFullYear().toString() + month.toString() + day + hour + minutes + seconds + (Math.round(Math.random() * 999 + 1000)).toString();
            };
            abp.dataleftComplete=(val)=>{
                return parseInt(val)<10?"0"+val:val;
            };
            abp.downloadfile=(data,filename)=>{
                if (!data) {
                    return;
                }
                let url = window.URL.createObjectURL(new Blob([data]))
                let link = document.createElement('a')
                link.style.display = 'none'
                link.href = url
                link.setAttribute('download', filename)

                document.body.appendChild(link)
                link.click()
            };
        }
    });
})