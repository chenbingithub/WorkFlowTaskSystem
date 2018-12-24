import Cookies from 'js-cookie';
import Util from '@/libs/util'
import appconst from '@/libs/appconst'
const user = {
    namespaced:true,
    state: {
        users:[],
        totalCount:0,
        pageSize:10,
        currentPage:1,
        organzitionId:"",
        seachKey:"",
        roles:[],
        pers:[],
        organ:[]
    },
    mutations: {
        
        logout (state, vm) {
            Cookies.remove('user');
            Cookies.remove('password');
            Cookies.remove('access');
            abp.auth.clearToken();
            location.reload();
            // 恢复默认样式
            let themeLink = document.querySelector('link[name="theme"]');
            themeLink.setAttribute('href', '');
            // 清空打开的页面等数据，但是保存主题数据
            let theme = '';
            if (localStorage.theme) {
                theme = localStorage.theme;
            }
            localStorage.clear();
            if (theme) {
                localStorage.theme = theme;
            }
        },
        setPageSize(state,size){
            state.pageSize=size;
        },
        setCurrentPage(state,page){
            state.currentPage=page;
        },
        setSeachKey(state,seachKey){
            state.seachKey=seachKey;
            state.organzitionId="";
        },
        setOrganzitionId(state,organzitionId){
            state.organzitionId=organzitionId;
            state.seachKey="";
        },
    },
    actions:{
        async login({state},payload){
            let rep=await Util.ajax.post("/api/TokenAuth/Authenticate",payload.data);  
        },
        async loginWindow({state},payload){
            return await Util.ajax.post("/api/TokenAuth/AuthenticateWindows"); 
        },
        async changepwd({state},payload){
            let rep=await Util.ajax.post("/api/TokenAuth/changepwd",payload.data);
            
        },
        async getAll({state},payload){
            let page={
                maxResultCount:state.pageSize,
                skipCount:(state.currentPage-1)*state.pageSize,
                seachKey:state.seachKey,
                organzitionId:state.organzitionId
            }
            let rep= await Util.ajax.get('/api/services/app/User/GetAllOrSeach',{params:page});
            state.users=[];
            state.users.push(...rep.data.result.items);
            state.totalCount=rep.data.result.totalCount;
        },       
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/User/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/User/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/User/Update',payload.data);
        },
        async getuser({state},payload){
            let page={
                userId:payload.data
            }
            let rep=await Util.ajax.get('/api/services/app/User/GetRolePersOrgan',{params:page});
            state.roles=[];
            state.roles.push(...rep.data.result.roles);
            state.organ=[];
            state.organ.push(...rep.data.result.organzitions);
            state.pers=[];
            state.pers.push(...rep.data.result.permissions);
        },
        async changeLanguage({state},payload){
            let rep=await Util.ajax.post('/api/services/app/User/ChangeLanguage',payload.data);
            abp.utils.setCookieValue(
                'Abp.Localization.CultureName',
                payload.data.languageName,
                new Date(new Date().getTime() + 5 * 365 * 86400000),
                abp.appPath
            );  
            window.location.reload();
        }
    }
};

export default user;
