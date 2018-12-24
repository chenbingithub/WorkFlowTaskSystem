import Cookies from 'js-cookie';
import Util from '@/libs/util'
const plotratio = {
    namespaced:true,
    state: {
        plotratios:[],
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
            let rep= await Util.ajax.get('/api/services/app/plotratio/GetAll',{params:page});
            state.plotratios=[];
            state.plotratios.push(...rep.data.result.items);
            state.totalCount=rep.data.result.totalCount;
        },
        async deleteAll({state},payload){
            await Util.ajax.post('/api/services/app/plotratio/RealDeleteAll');
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/plotratio/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/plotratio/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/plotratio/Update',payload.data);
        },
        
    }
};

export default plotratio;
