import Cookies from 'js-cookie';
import Util from '@/libs/util'
const cableconstant = {
    namespaced:true,
    state: {
        cableconstants:[],
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
            let rep= await Util.ajax.get('/api/services/app/cableconstant/GetAll',{params:page});
            state.cableconstants=[];
            state.cableconstants.push(...rep.data.result.items);
            state.totalCount=rep.data.result.totalCount;
        },
        async deleteAll({state},payload){
            await Util.ajax.post('/api/services/app/cableconstant/RealDeleteAll');
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/cableconstant/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/cableconstant/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/cableconstant/Update',payload.data);
        },
        async Insert({state},payload){
             await Util.ajax.post('/api/services/app/cableconstant/Insert',payload.data);
        },
    }
};

export default cableconstant;
