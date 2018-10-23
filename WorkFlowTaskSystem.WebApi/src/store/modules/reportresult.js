import Cookies from 'js-cookie';
import Util from '@/libs/util'
const reportresult = {
    namespaced:true,
    state: {
        reportresults:[],
        totalCount:0,
        pageSize:10,
        currentPage:1,
    },
    mutations: {
        setPageSize(state,size){
            state.pageSize=size;
        },
        setCurrentPage(state,page){
            state.currentPage=page;
        }
    },
    actions:{
        async getAll({state},payload){
            let page={
                maxResultCount:state.pageSize,
                skipCount:(state.currentPage-1)*state.pageSize
            }
            let rep= await Util.ajax.get('/api/services/app/reportresult/GetAll',{params:page});
            state.reportresults=[];
            state.reportresults.push(...rep.data.result.items);
            state.totalCount=rep.data.result.totalCount;
        },
        async deleteAll({state},payload){
            await Util.ajax.post('/api/services/app/reportresult/RealDeleteAll');
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/reportresult/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/reportresult/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/reportresult/Update',payload.data);
        },
        async Insert({state},payload){
             await Util.ajax.post('/api/services/app/reportresult/Insert',payload.data);
        },
         async download({state},payload){//下载
            return await Util.ajaxfile.post('/FileHelper/DownLoad?file='+payload.data.file);
        },
         async reportdownload({state},payload){//下载
            return await Util.ajaxfile.post('/FileHelper/CalculateReport?numberNo='+payload.data.numberNo);
        },
    }
};

export default reportresult;
