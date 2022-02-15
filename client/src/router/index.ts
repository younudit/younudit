import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import ApVouchers from '../views/ApVouchers.vue'
import CustomerDocuments from '../views/CustomerDocuments.vue'
import Easements from '../views/Easements.vue'
import EngineeringCounterLogs from '../views/EngineeringCounterLogs.vue'
import MeterApplications from '../views/MeterApplications.vue'
import PlatMats from '../views/PlatMats.vue'
import RecordDrawings from '../views/RecordDrawings.vue'
import SideSewerPermits from '../views/SideSewerPermits.vue'
import UlidTsheets from '../views/UlidTsheets.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/',
        name: 'ap-vouchers',
        component: ApVouchers
    },
    {
        path: '/ap-vouchers',
        name: 'ap-vouchers',
        component: ApVouchers
    },
    {
        path: '/customer-documents',
        name: 'customer-documents',
        component: CustomerDocuments
    },
    {
        path: '/engineering-counter-logs',
        name: 'engineering-counter-logs',
        component: EngineeringCounterLogs
    },
    {
        path: '/side-sewer-permits',
        name: 'side-sewer-permits',
        component: SideSewerPermits
    },
    {
        path: '/meter-applications',
        name: 'meter-applications',
        component: MeterApplications
    },
    {
        path: '/record-drawings',
        name: 'record-drawings',
        component: RecordDrawings
    },
    {
        path: '/easements',
        name: 'easements',
        component: Easements
    },
    {
        path: '/plat-maps',
        name: 'plat-maps',
        component: PlatMats
    },
    {
        path: '/ulid-t-sheets',
        name: 'ulid-t-sheets',
        component: UlidTsheets
    },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
