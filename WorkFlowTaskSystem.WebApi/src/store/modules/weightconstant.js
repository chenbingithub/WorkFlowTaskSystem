import Cookies from 'js-cookie';
import Util from '@/libs/util'
const weightconstant = {
    namespaced:true,
    state: {
        weightconstants:[],
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
                skipCount:(state.currentPage-1)*state.pageSize,
                seachKey:state.seachKey,
            }
            let rep= await Util.ajax.get('/api/services/app/weightconstant/GetAll',{params:page});
            state.weightconstants=[];
            state.weightconstants.push(...rep.data.result.items);
            state.totalCount=rep.data.result.totalCount;
        },
        async deleteAll({state},payload){
            await Util.ajax.post('/api/services/app/weightconstant/RealDeleteAll');
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/weightconstant/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/weightconstant/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/weightconstant/Update',payload.data);
        },
        
    }
};

export default weightconstant;
