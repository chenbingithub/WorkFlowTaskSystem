import Cookies from 'js-cookie';
import Util from '@/libs/util'
const bridgeconstant = {
    namespaced:true,
    state: {
        bridgeconstants:[],
        totalCount:0,
        pageSize:10,
        currentPage:1,
        seachKey:"",
    },
    mutations: {
        setPageSize(state,size){
            state.pageSize=size;
        },
        setCurrentPage(state,page){
            state.currentPage=page;
        },
        setSeachKey(state,seachKey){
            state.seachKey=seachKey;
        },
    },
    actions:{
        async getAll({state},payload){
            let page={
                maxResultCount:state.pageSize,
                skipCount:(state.currentPage-1)*state.pageSize,
                seachKey:state.seachKey,
            }
            let rep= await Util.ajax.get('/api/services/app/bridgeconstant/GetAllOrSeach',{params:page});
            state.bridgeconstants=[];
            state.bridgeconstants.push(...rep.data.result.items);
            state.totalCount=rep.data.result.totalCount;
        },
        async deleteAll({state},payload){
            await Util.ajax.post('/api/services/app/bridgeconstant/RealDeleteAll');
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/bridgeconstant/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/bridgeconstant/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/bridgeconstant/Update',payload.data);
        },
        async Insert({state},payload){
             await Util.ajax.post('/api/services/app/bridgeconstant/Insert',payload.data);
        },
    }
};

export default bridgeconstant;
